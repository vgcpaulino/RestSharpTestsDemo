using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharpTestsDemo.Helpers;
using System.Text.RegularExpressions;

namespace RestSharpTestsDemo.RestfulBooker
{
    public class GetBookingIdsTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/booking";

        private readonly RestClient restClient;
        private RestRequest restRequest;
        public IRestResponse restResponse;
        
        private readonly ResponseParser parser;

        public int numericStatusCode;
        public string responseBody;
       
        public GetBookingIdsTests()
        {
            parser = new ResponseParser();
            
            // Set the base URL;
            restClient = new RestClient($"{APIurl}");            
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

            // Verify the "Body" and "Status Code";
            numericStatusCode = parser.GetStatusCode(restResponse); 
            Assert.AreEqual(200, numericStatusCode);
            
            responseBody = restResponse.Content;
            int qtyRootItems = Regex.Matches(responseBody, "\"bookingid\"").Count; 
            Assert.Greater(qtyRootItems, 0);            
        }

    }
}
