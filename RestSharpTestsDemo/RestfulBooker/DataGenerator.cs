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
        
        private const string APIurl = "https://restful-booker.herokuapp.com/booking";

        private readonly RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;
        
        private readonly ResponseParser parser;
        public readonly object jsonBodyObj;
        public string newBookingId;

        public DataGenerator()
        {
            // Set the base URL;
            restClient = new RestClient($"{APIurl}");

            parser = new ResponseParser();

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

            NewBooking();
        }

        private void NewBooking()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.POST);

            // Set the Header info;
            restRequest.AddHeader("Accept", "*/*");
            
            // Set the Body info;
            restRequest.AddJsonBody(jsonBodyObj);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Set the newBookingId variable;
            newBookingId = parser.GetPropertieValue(restResponse, "bookingid");
        }

    }
}
