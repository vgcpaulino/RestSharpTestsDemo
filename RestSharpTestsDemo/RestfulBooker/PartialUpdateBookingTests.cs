using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;
using RestSharpTestsDemo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestSharpTestsDemo.RestfulBooker
{
    public class PartialUpdateBookingTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/booking";

        private readonly RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;

        private readonly JsonOps json;
        private readonly ResponseParser parser;
        private readonly DataGenerator testData;      
        
        private readonly string newBookingId;
        private readonly object bookingReqObj;


        private int responseStatusCode; 
        private readonly object requestJsonObj;
        private JObject expectedJOject;
        private JObject responseJObject;

        public PartialUpdateBookingTests()
        {
            json = new JsonOps();
            parser = new ResponseParser();
            testData = new DataGenerator();

            newBookingId = testData.GetNewBooking();
            bookingReqObj = testData.jsonBodyObj;
            expectedJOject = json.ObjToJObject(bookingReqObj);

            requestJsonObj = new
            {
                additionalneeds = "Dinner"
            };

            // Set the base URL;
            restClient = new RestClient($"{APIurl}/{newBookingId}");
        }

        /* When using the AddJsonBody method from the RestRequest class
         * the request must not have the "Content-Type" header;
         * http://restsharp.org/usage/parameters.html#request-body
         */
        [Test]
        public void UpdateBooking_Auth_UsingAddJsonBodyMethod_PATCH()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.PATCH);

            // Set the Header info;
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            // Set the Body info;
            restRequest.AddJsonBody(requestJsonObj);

            // Call the API;
            restResponse = restClient.Execute(restRequest);           

            // Verify the "Body" and "Status Code";
            responseStatusCode = parser.GetStatusCode(restResponse);
            Assert.AreEqual(200, responseStatusCode);

            responseJObject = json.StrToJObject(restResponse.Content);
            expectedJOject = json.MergeJsonObject(expectedJOject, responseJObject);
            bool resultBodyComparsion = json.CompareRequestAndResponseJson(expectedJOject, responseJObject);
            Assert.True(resultBodyComparsion);
        }

    }
}
