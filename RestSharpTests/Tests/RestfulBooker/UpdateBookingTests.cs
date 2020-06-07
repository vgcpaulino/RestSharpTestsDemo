using RestSharpTests.ClientHelper;
using RestSharpTests.Entities.RestfulBooker;
using RestSharpTests.Helpers;
using Xunit;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class UpdateBookingTests : BaseTests
    {
        public UpdateBookingTests()
        {
            // Set the API endpoint and request method;
            HttpClient.Url = TestUrl.BookingWithId(1);
            HttpClient.Method = "PATCH";
            
            // Set the Header information;
            HttpClient.AddHeader("Content-type", "application/json");
            HttpClient.AddHeader("Accept", "application/json");
            HttpClient.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            // Create BookingModel object;
            BookingModel.FirstName = "John";
            BookingModel.LastName = "Doe";
            BookingModel.TotalPrice = 1110;
            BookingModel.DepositPaid = true;
            BookingModel.BookingDates.Checkin = "2020-01-01";
            BookingModel.BookingDates.Checkout = "2021-01-01";
            BookingModel.AdditionalNeeds = "Breakfest";
        }

        [Fact]
        public void UpdateBooking_Auth_PUT()
        {
            // Add the Json Body;
            HttpClient.AddJsonBody(BookingModel);

            // Execute the API request;
            HttpClient.Execute();

            // Verification;
            Assert.Equal(200, HttpClient.ResponseStatusCode);
            Assert.Equal("OK", HttpClient.ResponseStatusMsg);
            Assert.True(Compare.CompareTwoJObjects(BookingModel, HttpClient.ResponseContent));
        }

    }
}
