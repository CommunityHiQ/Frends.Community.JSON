using System;
using System.ComponentModel;
using System.Threading;
using Newtonsoft.Json.Linq;
using JUST;

#pragma warning disable 1591

namespace Frends.Community.Json
{
    public class JsonTasks
    {
        /// <summary>
        /// This task allows enforcing types in Json documents by giving an array of
        /// Json paths and corresponding Json types.
        /// Documentation: https://github.com/CommunityHiQ/Frends.Community.Json#EnforceJsonTypes
        /// </summary>
        /// <returns>Object { string Result }</returns>
        public static string EnforceJsonTypes(EnforceJsonTypesInput input, CancellationToken cancellationToken)
        {
            var jObject = JObject.Parse(input.Json);
            foreach (var rule in input.Rules)
            {
                foreach (var jToken in jObject.SelectTokens(rule.JsonPath))
                {
                    ChangeDataType(jToken, rule.DataType);
                }
            }

            return jObject.ToString();
        }

        /// <summary>
        /// Maps input json using JUST.Net library. 
        /// JsonMapper Task documentation: 'https://github.com/CommunityHiQ/Frends.Community.Json#JsonMapper'
        /// JUST.Net documentation: 'https://github.com/WorkMaze/JUST.net'
        /// </summary>
        /// <returns>Object { string Result, JToken ToJson() }</returns>
        public static JsonMapperResult JsonMapper(JsonMapperInput input, CancellationToken cancellationToken)
        {
            string result = string.Empty;
            //Try parse input Json for simple validation
            try
            {
                JToken.Parse(input.InputJson.ToString());
            }
            catch (Exception ex)
            {
                throw new FormatException("Input Json is not valid: " + ex.Message);
            }
            try
            {
                result = JsonTransformer.Transform(input.JsonMap, input.InputJson.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Json transformation failed: " + ex.Message, ex);
            }

            return new JsonMapperResult(result);
        }



        /// <summary>
        /// Changes value of JValue object to the desired Json data type
        /// </summary>
        /// <returns></returns>
        public static void ChangeDataType(JToken value, JsonDataType dataType)
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
                        throw new Exception($"Unknown Json data type {dataType}");
                }

                if (dataType != JsonDataType.Array) value.Value = newValue;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
