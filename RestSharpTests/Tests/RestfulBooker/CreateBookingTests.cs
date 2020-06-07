using System.Collections.Generic;
using Xunit;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class CreateBookingTests : BaseTests
    {
        public CreateBookingTests()
        {
            // Set the API endpoint and request method;
            HttpClient.Url = TestUrl.Booking;
            HttpClient.Method = "POST";

            // Set the Header information;
            HttpClient.AddHeader("Content-Type", "application/json");

            // Create BookingModel object;
            BookingModel.FirstName = "John";
            BookingModel.LastName = "Doe";
            BookingModel.TotalPrice = 1110;
            BookingModel.DepositPaid = true;
            BookingModel.BookingDates.Checkin = "2020-01-01";
            BookingModel.BookingDates.Checkout = "2021-01-01";
            BookingModel.AdditionalNeeds = "Breakfest";

            // Create BookingModel response object;
            BookingRespModel.BookingId = 0;
            BookingRespModel.Booking = BookingModel;
        }

        [Fact]
        public void CreateBooking_RequireAccept_Post()
        {
            // Add the Json Body;
            HttpClient.AddJsonBody(BookingModel);

            // Execute the API request;
            HttpClient.Execute();

            // Verification;
            Assert.Equal(418, HttpClient.ResponseStatusCode);
            Assert.Equal("I'm a teapot", HttpClient.ResponseStatusMsg);
            Assert.Equal("I'm a teapot", HttpClient.ResponseContent);
        }

        [Fact]
        public void CreateBooking_Post() {
            // Add the Header parameters;
            HttpClient.AddHeader("Accept", "*/*");

            // Add the Json Body;
            HttpClient.AddJsonBody(BookingModel);

            // Execute the API request;
            HttpClient.Execute();

            // Verification;
            Assert.Equal(200, HttpClient.ResponseStatusCode);
            Assert.Equal("OK", HttpClient.ResponseStatusMsg);
            string bookingid = Search.GetPropertyValue(HttpClient.ResponseContent, "bookingid");
            Assert.NotNull(bookingid);

            List<string> ignoredFields = new List<string>() { "bookingid" };
            Assert.True(Compare.CompareTwoJObjects(BookingRespModel, HttpClient.ResponseContent, false, ignoredFields));
        }
        
    }
}
