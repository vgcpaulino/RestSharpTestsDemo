using RestSharpTests.ClientHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class GetBookingIdsTests : BaseTests
    {        
        public GetBookingIdsTests()
        {
            // Set the API endpoint and request method;
            HttpClient.Url = TestUrl.Booking;
            HttpClient.Method = "GET";

            // Set the Header information;
            HttpClient.AddHeader("Content-Type", "application/json");
            HttpClient.AddHeader("Accept", "application/json");
        }

        [Fact]
        public void GetAllBookingIds()
        {
            // Execute the API request;
            HttpClient.Execute();

            // Verification;
            Assert.Equal(200, HttpClient.ResponseStatusCode);
            Assert.Equal("OK", HttpClient.ResponseStatusMsg);
            int qtyRootItems = Regex.Matches(HttpClient.ResponseContent, "\"bookingid\"").Count;
            Assert.True(qtyRootItems > 0);
        }
    }
}
