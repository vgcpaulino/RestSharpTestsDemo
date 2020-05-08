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
    public class UpdateBookingTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/booking/1";

        private readonly RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;
        
        private readonly ResponseParser parser;
        private readonly JsonOps json;
        
        private int numericStatusCode; 
        private readonly object requestJsonObj;
        private readonly string requestJsonStr;
        private JObject expectedJOject;
        private JObject responseJObject;

        public UpdateBookingTests()
        {
            parser = new ResponseParser();
            json = new JsonOps();

            requestJsonObj = new
            {
                firstname = "John",
                lastname = "Doe",
                totalprice = 1110,
                depositpaid = true,
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
        public void UpdateBooking_Auth_PUT()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.PUT);

            // Set the Header info;
            restRequest.AddHeader("Content-type", "application/json");
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            // Set the Body info;
            restRequest.AddParameter("application/json,text/plain", requestJsonStr, ParameterType.RequestBody);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Verify the "Body" and "Status Code";
            numericStatusCode = parser.GetStatusCode(restResponse); 
            Assert.AreEqual(200, numericStatusCode);

            expectedJOject = json.ObjToJObject(requestJsonObj);
            responseJObject = json.StrToJObject(restResponse.Content);
            Assert.True(json.CompareRequestAndResponseJson(expectedJOject, responseJObject));
        }

        /* When using the AddJsonBody method from the RestRequest class
         * the request must not have the "Content-Type" header;
         * http://restsharp.org/usage/parameters.html#request-body
         */
        [Test]
        public void UpdateBooking_Auth_UsingAddJsonBodyMethod_PUT()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.PUT);

            // Set the Header info;
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            // Set the Body info;
            restRequest.AddJsonBody(requestJsonObj);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Verify the "Body" and "Status Code";
            numericStatusCode = parser.GetStatusCode(restResponse);
            Assert.AreEqual(200, numericStatusCode); 
            
            expectedJOject = json.ObjToJObject(requestJsonObj);
            responseJObject = json.StrToJObject(restResponse.Content);
            Assert.True(json.CompareRequestAndResponseJson(expectedJOject, responseJObject));
        }
   
    }
}
