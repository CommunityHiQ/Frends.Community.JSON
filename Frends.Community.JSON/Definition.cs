#pragma warning disable 1591
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace Frends.Community.JSON
{
    public class EnforceJsonTypesInput
    {
        /// <summary>
        /// The JSON string to be processed.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// JSON data type rules to enforce.
        /// </summary>
        public JsonTypeRule[] Rules { get; set; }
    }

    public class JsonMapperInput
    {
        /// <summary>
        /// Map input json in String or JToken type
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("{\"name\":\"veijo\"}")]
        public object InputJson { get; set; }

        /// <summary>
        /// JUST json map. See: https://github.com/WorkMaze/JUST.net#just
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("{\"firstName\":\"#valueof($.name)\"}")]
        public string JsonMap { get; set; }
    }
    public class JsonMapperResult
    {
        /// <summary>
        /// Transformation result as string
        /// </summary>
        public string Result { get; set; }

        private readonly Lazy<JToken> _jToken;

        public JsonMapperResult(string transformationResult)
        {
            Result = transformationResult;

            _jToken = new Lazy<JToken>(() => ParseJson(Result));
        }

        /// <summary>
        /// Get transformation result as JToken
        /// </summary>
        public JToken ToJson() { return _jToken.Value; }


        private static JToken ParseJson(string jsonString)
        {
            return JToken.Parse(jsonString);
        }
    }
}
