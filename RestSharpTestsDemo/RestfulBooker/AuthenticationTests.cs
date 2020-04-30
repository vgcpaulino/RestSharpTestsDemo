using NUnit.Framework;
using RestSharp;
using RestSharpTestsDemo.Helpers;

namespace RestSharpTestsDemo.RestfulBooker
{
    public class AuthenticationTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/auth";
        private readonly RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;
        private string responseBody;
        private int numericStatusCode;
        private ResponseParser parser;

        public AuthenticationTests()
        {
            // Set the base URL;
            restClient = new RestClient($"{APIurl}");

            parser = new ResponseParser();
        }

        [Test]
        public void Authentication_RightCredentials_Post()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.POST);

            // Set the Header info;
            restRequest.AddHeader("Content-Type", "application/json");

            // Set the Parameters info;
            restRequest.AddJsonBody(new { username = "admin", password = "password123" });
            //restRequest.AddParameter("application/json", "{\r\n    \"username\" : \"admin\",\r\n    \"password\" : \"password123\"\r\n}", ParameterType.RequestBody);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Get the "Body" content and "Status Code";
            responseBody = restResponse.Content;
            numericStatusCode = parser.GetStatusCode(restResponse);

            // Verify the "Body" and "Status Code";
            Assert.That(responseBody.Contains("\"token\""));
            Assert.AreEqual(200, numericStatusCode);
        }

        [Test]
        public void Authentication_BadCredentials_Post()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.POST);

            // Set the Header info;
            restRequest.AddHeader("Content-Type", "application/json");

            // Set the Parameters info;
            restRequest.AddJsonBody(new { username = "admin", password = "password12" });

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Get the "Body" content and "Status Code";
            responseBody = restResponse.Content;
            numericStatusCode = parser.GetStatusCode(restResponse);

            // Verify the "Body" and "Status Code";
            Assert.That(responseBody.Contains("{\"reason\":\"Bad credentials\"}"));
            Assert.AreEqual(200, numericStatusCode);
        }

        [Test]
        public void Authentication_NoCredentials_Post()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.POST);

            // Set the Header info;
            restRequest.AddHeader("Content-Type", "application/json");

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Get the "Body" content and "Status Code";
            responseBody = restResponse.Content;
            numericStatusCode = parser.GetStatusCode(restResponse);

            // Verify the "Body" and "Status Code";
            Assert.That(responseBody.Contains("{\"reason\":\"Bad credentials\"}"));
            Assert.AreEqual(200, numericStatusCode);
        }


    }
}
