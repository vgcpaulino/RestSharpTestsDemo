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
        private string responseBody;
        private int numericStatusCode;
        private readonly ResponseParser parser;
        private readonly JsonOps jsonOps;
        private readonly Object jsonBody;
        private readonly string stringBody;

        public CreatingBookingTests()
        {
            // Set the base URL;
            restClient = new RestClient($"{APIurl}");

            parser = new ResponseParser();
            jsonOps = new JsonOps();

            jsonBody = new
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
            stringBody = jsonOps.ConvertObjToJson(jsonBody);
        }

        [Test]
        public void CreateBooking_RequireAccept_Post()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.POST);

            // Set the Header info;
            restRequest.AddHeader("Content-Type", "application/json");
                      
            // Set the Body info;
            restRequest.AddParameter("application/json,text/plain", stringBody, ParameterType.RequestBody);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Get the "Body" content and "Status Code";
            responseBody = restResponse.Content;
            numericStatusCode = parser.GetStatusCode(restResponse);

            // Verify the "Body" and "Status Code";
            Assert.That(!responseBody.Contains("bookingid"));
            Assert.AreEqual(418, numericStatusCode);
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
            restRequest.AddParameter("application/json,text/plain", stringBody, ParameterType.RequestBody);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Get the "Body" content and "Status Code";
            responseBody = restResponse.Content;
            numericStatusCode = parser.GetStatusCode(restResponse);

            // Verify the "Body" and "Status Code";
            Assert.That(responseBody.Contains("bookingid"));
            Assert.AreEqual(200, numericStatusCode);
        }

    }
}