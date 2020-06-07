using System;
using RestSharp;
using RestSharpTests.Helpers;

namespace RestSharpTests.ClientHelper
{
    public partial class APIClient : CompareHelper
    {
        private readonly RestClient restClient;
        private readonly RestRequest restRequest;
        private IRestResponse restRespose;

        private readonly ConversionHelper conversion;
        private readonly ResponseSearch responseParser;

        public APIClient()
        {
            restClient = new RestClient();
            restRequest = new RestRequest();

            conversion = new ConversionHelper();
            responseParser = new ResponseSearch();
        }

        public string Url { get; set; }
        public string Method { get; set; }
        private string JsonBody { get; set; }
        public int ResponseStatusCode { get; set; }
        public string ResponseStatusMsg { get; set; }
        public string ResponseContent { get; set; }

        public void AddHeader(string keyName, string keyValue)
        {
            restRequest.AddHeader(keyName, keyValue);
        }

        public void AddJsonBody(string jsonStr)
        {
            JsonBody = jsonStr;
        }

        public void AddJsonBody(object jsonObj)
        {
            JsonBody = conversion.ObjToString(jsonObj, true);
        }

        public void Execute()
        {
            restClient.BaseUrl = new Uri(Url);
            restRequest.Method = APIMethods.GetMethodObj(Method);
            
            if (JsonBody != null && JsonBody != "")
            {
                restRequest.AddJsonBody(JsonBody);
            }

            restRespose = restClient.Execute(restRequest);
            ResponseStatusCode = (int)restRespose.StatusCode;
            ResponseStatusMsg = restRespose.StatusDescription;
            ResponseContent = restRespose.Content;
        }

    }
}
