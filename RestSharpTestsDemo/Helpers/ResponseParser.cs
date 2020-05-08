using RestSharp;

namespace RestSharpTestsDemo.Helpers
{
    public class ResponseParser
    {

        private readonly JsonOps jsonOps;
        
        public ResponseParser()
        {
            jsonOps = new JsonOps();
        }

        public int GetStatusCode(IRestResponse restResponse)
        {
            return (int)restResponse.StatusCode;
        }

        public string GetPropertieValue(IRestResponse restResponse, string propertyName)
        {
            var responseJsonObj = jsonOps.StrToJObject(restResponse.Content);
            return (string)responseJsonObj[propertyName];
        }

    }
}
