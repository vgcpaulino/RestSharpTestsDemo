using Newtonsoft.Json.Linq;

namespace RestSharpTests.Helpers
{
    public class ResponseSearch
    {
        private readonly ConversionHelper conversion;

        public ResponseSearch()
        {
            conversion = new ConversionHelper();
        }

        public string GetPropertyValue(string responseContent, string propertyName)
        {
            JObject responseJObj = conversion.StringToJObject(responseContent);
            return (string)responseJObj[propertyName];
        }
    }
}
