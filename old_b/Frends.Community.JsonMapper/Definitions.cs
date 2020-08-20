using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Community.JsonMapper
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class InputProperties
    {
        /// <summary>
        /// Map input json in String or JToken type
        /// </summary>
        /// 
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

    public class TransformationResult
    {
        /// <summary>
        /// Transformation result as string
        /// </summary>
        public string Result { get; set; }

        private readonly Lazy<JToken> _jToken;

        public TransformationResult(string transformationResult)
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
