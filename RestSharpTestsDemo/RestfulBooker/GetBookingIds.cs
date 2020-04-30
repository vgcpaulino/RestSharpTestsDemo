using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharpTestsDemo.Helpers;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RestSharpTestsDemo.RestfulBooker
{
    public class GetBookingIds
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/booking";
        private readonly RestClient restClient;
        private RestRequest restRequest;
        public IRestResponse restResponse;
        public string responseBody;
        public int numericStatusCode;
        private readonly ResponseParser parser;
        private readonly JsonOps jsonOps;

        public GetBookingIds()
        {
            // Set the base URL;
            restClient = new RestClient($"{APIurl}");

            parser = new ResponseParser();
            jsonOps = new JsonOps();
        }

        [Test]
        public void GetAllBookingIds()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.GET);

            // Set the Header info;
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Get the "Body" content and "Status Code";
            responseBody = restResponse.Content;
            numericStatusCode = parser.GetStatusCode(restResponse);
            int qtyRootItems = Regex.Matches(responseBody, "\"bookingid\"").Count;
            
            // Verify the "Body" and "Status Code";
            Assert.Greater(qtyRootItems, 0);
            Assert.AreEqual(200, numericStatusCode);
        }


    }
}
