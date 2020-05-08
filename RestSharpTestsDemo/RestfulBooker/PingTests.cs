using NUnit.Framework;
using RestSharp;
using RestSharpTestsDemo.Helpers;

namespace RestSharpTestsDemo.RestfulBooker
{
    public class PingTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/ping";

        private readonly RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;
        
        private readonly ResponseParser parser;
        
        private int numericStatusCode;

        public PingTests()
        {
            parser = new ResponseParser();

            // Set the base URL;
            restClient = new RestClient($"{APIurl}");            
        }

        [Test]
        public void Ping()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.GET);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Verify the "Body" and "Status Code";
            numericStatusCode = parser.GetStatusCode(restResponse);
            Assert.AreEqual(201, numericStatusCode); 
            
            Assert.AreEqual("Created", restResponse.Content);
        }

    }
}
