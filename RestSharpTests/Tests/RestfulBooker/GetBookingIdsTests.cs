using RestSharpTests.ClientHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class GetBookingIdsTests
    {
        private const string APIurl = "https://restful-booker.herokuapp.com/booking"; 
        
        private readonly APIClient client;

        public GetBookingIdsTests()
        {
            client = new APIClient();
            
            // Set the API endpoint and request method;
            client.Url = APIurl;
            client.Method = "GET";

            // Set the Header information;
            client.AddHeader("Content-Type", "application/json");
            client.AddHeader("Accept", "application/json");
        }

        [Fact]
        public void GetAllBookingIds()
        {
            // Execute the API request;
            client.Execute();

            // Verification;
            Assert.Equal(200, client.ResponseStatusCode);
            Assert.Equal("OK", client.ResponseStatusMsg);
            int qtyRootItems = Regex.Matches(client.ResponseContent, "\"bookingid\"").Count;
            Assert.True(qtyRootItems > 0);
        }
    }
}
