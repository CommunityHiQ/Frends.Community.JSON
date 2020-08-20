using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Frends.Community.JSON.EnforceJSONTypes
{
    /// <summary>
    /// JsonTypeEnforcer contains the EnforceJsonTypes task
    /// </summary>
    public class JsonTypeEnforcer
    {
        /// <summary>
        /// This task allows enforcing types in JSON documents by giving an array of
        /// JSON paths and corresponding JSON types.
        /// Documentation: https://github.com/CommunityHiQ/Frends.Community.JSON.EnforceJSONTypes
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public static string EnforceJsonTypes(EnforceJsonTypesParameters parameters)
        {
            var jObject = JObject.Parse(parameters.Json);
            foreach (var rule in parameters.Rules)
            {
                foreach (var jToken in jObject.SelectTokens(rule.JsonPath))
                {
                    ChangeDataType(jToken, rule.DataType);
                }
            }

            return jObject.ToString();
        }

        /// <summary>
        /// Changes value of JValue object to the desired JSON data type
        /// </summary>
        /// <param name="value">JValue to change</param>
        /// <param name="dataType">New data type</param>
        /// <returns></returns>
        internal static void ChangeDataType(JToken value, JsonDataType dataType)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (dataType == JsonDataType.Array)
            {
                ChangeJTokenIntoArray(value);
                return;
            }

            var jValue = value as JValue;
            if (jValue == null)
            {
                throw new Exception($"This task can only convert JValue nodes' types and turn JTokens into JArrays, but the node type provided is {value.GetType().Name}");
            }

            ChangeDataTypeSimple(jValue, dataType);
        }

        private static void ChangeJTokenIntoArray(JToken jToken)
        {
            if (jToken is JArray) return;
            var jProperty = jToken.Parent as JProperty;
            if (jProperty != null)
            {
                var jArray = new JArray();
                jArray.Add(jToken);
                jProperty.Value = jArray;
            }
        }

        private static void ChangeDataTypeSimple(JValue value, JsonDataType dataType)
        {
            object newValue = value.Value;
            try
            {
                switch (dataType)
                {
                    case JsonDataType.String:
                        newValue = value.Value<string>();
                        break;
                    case JsonDataType.Number:
                        if (value.Value == null || (value.Value as string) == "") newValue = null;
                        else
                        {
                            var stringValue = value.Value<string>();
                            if (stringValue.Contains(".")) newValue = value.Value<double>();
                            else newValue = value.Value<int>();
                        }
                        break;
                    case JsonDataType.Boolean:
                        if (value.Value == null || (value.Value as string) == "") newValue = null;
                        else newValue = value.Value<bool>();
                        break;
                    case JsonDataType.Array:
                        // Here we actually need to replace the JValue with a JArray that would contain the current JValue
                        var jProperty = value.Parent as JProperty;
                        if (jProperty != null)
                        {
                            var jArray = new JArray();
                            jArray.Add(value);
                            jProperty.Value = jArray;
                        }

                        break;
                    default:
                        throw new Exception($"Unknown JSON data type {dataType}");
                }

                if (dataType != JsonDataType.Array) value.Value = newValue;
            }
            catch
            {
                // do nothing
            }
        }
    }
}
