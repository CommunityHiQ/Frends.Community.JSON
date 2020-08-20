#pragma warning disable 1591

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Community.JSON
{
    /// <summary>
    /// Parameters for EnforceJsonTypes task
    /// </summary>
    public class EnforceJsonTypesInput
    {
        /// <summary>
        /// JSON document to process
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// JSON data type rules to enforce
        /// </summary>
        public JsonTypeRule[] Rules { get; set; }
    }

    public class EnforceJsonTypesResult
    {
        public EnforceJsonTypesResult(string json)
        {
            Result = json;
        }

        public string Result { get;  }
    }
}
