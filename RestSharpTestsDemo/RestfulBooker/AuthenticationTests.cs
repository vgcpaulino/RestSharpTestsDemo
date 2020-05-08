using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Extensions;
using RestSharpTestsDemo.Helpers;

namespace RestSharpTestsDemo.RestfulBooker
{
    public class AuthenticationTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/auth";

        private readonly RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;

        private readonly JsonOps json;
        private readonly ResponseParser parser;

        private readonly object requestJsonObj;
        private int numericStatusCode;
        private JObject responseJObject;
        
        public AuthenticationTests()
        {
            json = new JsonOps();
            parser = new ResponseParser();

            requestJsonObj = new
            {
                username = "admin",
                password = "password123"
            };

            // Set the base URL;
            restClient = new RestClient($"{APIurl}");           
        }

        [Test]
        public void Authentication_RightCredentials_Post()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.POST);

            // Set the Header info;
            restRequest.AddHeader("Content-Type", "application/json");

            // Set the Parameters info;
            restRequest.AddJsonBody(requestJsonObj);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Verify the "Body" and "Status Code";
            numericStatusCode = parser.GetStatusCode(restResponse);
            Assert.AreEqual(200, numericStatusCode);

            responseJObject = json.StrToJObject(restResponse.Content);
            string token = (string)responseJObject["token"];
            Assert.IsNotNull(token);
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

            // Verify the "Body" and "Status Code";
            numericStatusCode = parser.GetStatusCode(restResponse);
            Assert.AreEqual(200, numericStatusCode);

            responseJObject = json.StrToJObject(restResponse.Content);
            string responseMessage = (string)responseJObject["reason"];
            Assert.AreEqual("Bad credentials", responseMessage);          
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

            // Verify the "Body" and "Status Code";
            numericStatusCode = parser.GetStatusCode(restResponse);
            Assert.AreEqual(200, numericStatusCode);

            responseJObject = json.StrToJObject(restResponse.Content);
            string responseMessage = (string)responseJObject["reason"];
            Assert.AreEqual("Bad credentials", responseMessage);            
        }

    }
}
