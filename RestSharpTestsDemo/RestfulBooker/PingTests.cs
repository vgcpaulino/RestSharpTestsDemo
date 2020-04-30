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
        private string responseBody;
        private int numericStatusCode;
        private readonly ResponseParser parser;

        public PingTests()
        {
            // Set the base URL;
            restClient = new RestClient($"{APIurl}");

            parser = new ResponseParser();
        }

        [Test]
        public void Ping()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.GET);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Get the "Body" content and "Status Code";
            responseBody = restResponse.Content;
            numericStatusCode = parser.GetStatusCode(restResponse);

            // Verify the "Body" and "Status Code";
            Assert.AreEqual("Created", responseBody);
            Assert.AreEqual(201, numericStatusCode);
        }

    }
}
