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
    public class DataGenerator
    {
        
        private const string NewBookingAPIurl = "https://restful-booker.herokuapp.com/booking";
        private const string NewTokenAPIurl = "https://restful-booker.herokuapp.com/auth";

        private RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;
        
        private readonly ResponseParser parser;
        public object jsonBodyObj;
        private string newBookingId;
        private string newToken;

        public DataGenerator()
        {
            parser = new ResponseParser();
        }

        public string GetNewBooking()
        {
            // Set the base URL;
            restClient = new RestClient($"{NewBookingAPIurl}");

            // Set the Request Method;
            restRequest = new RestRequest(Method.POST);

            // Set the Header info;
            restRequest.AddHeader("Accept", "*/*");

            // Set the Body info;
            jsonBodyObj = new
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
            restRequest.AddJsonBody(jsonBodyObj);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Set the newBookingId variable;
            newBookingId = parser.GetPropertieValue(restResponse, "bookingid");
            return newBookingId;
        }

        public string GetTokenId()
        {
            // Set the base URL;
            restClient = new RestClient($"{NewTokenAPIurl}");
            
            // Set the Request Method;
            restRequest = new RestRequest(Method.POST);

            // Set the Header info;
            restRequest.AddHeader("Content-Type", "application/json");

            // Set the Body info;
            jsonBodyObj = new
            {
                username = "admin",
                password = "password123"
            };
            restRequest.AddJsonBody(jsonBodyObj);

            // Call the API;
            restResponse = restClient.Execute(restRequest);
            
            // Set the newBookingId variable;
            newToken = parser.GetPropertieValue(restResponse, "token");
            return newToken;
        }

    }
}
