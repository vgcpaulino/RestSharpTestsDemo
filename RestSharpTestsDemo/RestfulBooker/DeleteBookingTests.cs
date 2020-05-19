using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharpTestsDemo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsDemo.RestfulBooker
{
    public class DeleteBookingTests
    {
        
        private const string APIurl = "https://restful-booker.herokuapp.com/booking";

        private RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;

        private readonly JsonOps json;
        private readonly ResponseParser parser;
        private readonly DataGenerator testData;

        private string newBookingId;
        private string tokenId;
        private object bookingReqObj;


        private int responseStatusCode;
        private readonly object requestJsonObj;
        private JObject expectedJOject;
        private JObject responseJObject;

        public DeleteBookingTests()
        {
            json = new JsonOps();
            parser = new ResponseParser();
            testData = new DataGenerator();

            /*newBookingId = testData.newBookingId;
            bookingReqObj = testData.jsonBodyObj;
            expectedJOject = json.ObjToJObject(bookingReqObj);

            // Set the base URL;
            restClient = new RestClient($"{APIurl}/{newBookingId}");*/
        }

        [SetUp]
        public void TestSetUp()
        {
            newBookingId = testData.GetNewBooking();
            tokenId = testData.GetTokenId();

            // Set the base URL;
            restClient = new RestClient($"{APIurl}/{newBookingId}");
        }

        [Test]
        public void DeleteBooking_Auth_Delete()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.DELETE);

            // Set the Header info;
            restRequest.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

             // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Verify the "Body" and "Status Code";
            responseStatusCode = parser.GetStatusCode(restResponse);
            Assert.AreEqual(201, responseStatusCode);

            bool resultBodyComparsion = restResponse.Content.Contains("Created");
            Assert.True(resultBodyComparsion);
        }

        [Test]
        public void DeleteBooking_Cookie_Delete()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.DELETE);

            // Set the Header info;
            restRequest.AddHeader("Cookie", "token=" + tokenId);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Verify the "Body" and "Status Code";
            responseStatusCode = parser.GetStatusCode(restResponse);
            Assert.AreEqual(201, responseStatusCode);

            bool resultBodyComparsion = restResponse.Content.Contains("Created");
            Assert.True(resultBodyComparsion);
        }

    }
}
