using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharpTestsDemo.Helpers;
using System;

namespace RestSharpTestsDemo.RestfulBooker
{
    public class CreatingBookingTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/booking";

        private readonly RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;

        private readonly JsonOps json;
        private readonly ResponseParser parser;

        private int responseStatusCode; 
        private readonly object requestJsonObj;
        private readonly string requestJsonStr;
        private JObject responseJObject;

        public CreatingBookingTests()
        {
            parser = new ResponseParser();
            json = new JsonOps();

            requestJsonObj = new
            {
                firstname = "John",
                lastname = "Doe",
                totalprice = "1110",
                depositpaid = "true",
                bookingdates = new
                {
                    checkin = "2020-01- 01",
                    checkout = "2021-01-01"
                },
                additionalneeds = "Breakfast"
            };
            requestJsonStr = json.ObjToString(requestJsonObj);

            // Set the base URL;
            restClient = new RestClient($"{APIurl}");
        }

        [Test]
        public void CreateBooking_RequireAccept_Post()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.POST);

            // Set the Header info;
            restRequest.AddHeader("Content-Type", "application/json");
                      
            // Set the Body info;
            restRequest.AddParameter("application/json,text/plain", requestJsonStr, ParameterType.RequestBody);

            // Call the API;
            restResponse = restClient.Execute(restRequest);
            
            // Verify the "Body" and "Status Code";
            responseStatusCode = parser.GetStatusCode(restResponse);
            Assert.AreEqual(418, responseStatusCode);
            Assert.That(!restResponse.Content.Contains("bookingid"));
        }

        [Test]
        public void CreateBooking_Post()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.POST);

            // Set the Header info;
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Accept", "*/*");

            // Set the Body info;
            restRequest.AddParameter("application/json,text/plain", requestJsonStr, ParameterType.RequestBody);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Verify the "Body" and "Status Code";
            responseStatusCode = parser.GetStatusCode(restResponse);
            Assert.AreEqual(200, responseStatusCode);

            responseJObject = json.StrToJObject(restResponse.Content);
            string bookingIdValue = (string)responseJObject["bookingid"];
            Assert.IsNotNull(bookingIdValue);
        }

    }
}