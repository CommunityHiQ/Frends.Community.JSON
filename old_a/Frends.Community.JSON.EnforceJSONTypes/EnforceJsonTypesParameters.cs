namespace Frends.Community.JSON.EnforceJSONTypes
{
    /// <summary>
    /// Parameters for EnforceJsonTypes task
    /// </summary>
    public class EnforceJsonTypesParameters
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
}