using JUST;
using Newtonsoft.Json.Linq;
using System;

namespace Frends.Community.JsonMapper
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class JsonMapperTask
    {
        /// <summary>
        /// Maps input json using JUST.Net library. 
        /// JsonMapper Task documentation: 'https://github.com/CommunityHiQ/Frends.Community.JsonMapper'
        /// JUST.Net documentation: 'https://github.com/WorkMaze/JUST.net#just'
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Object { string Result, JToken ToJson() }</returns>
        public static TransformationResult Transform(InputProperties input)
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

            return new TransformationResult(result);
        }
    }
}
