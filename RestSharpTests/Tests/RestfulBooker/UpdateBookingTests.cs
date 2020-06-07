using RestSharpTests.ClientHelper;
using RestSharpTests.Entities.RestfulBooker;
using RestSharpTests.Helpers;
using Xunit;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class UpdateBookingTests
    {
        private const string APIurl = "https://restful-booker.herokuapp.com/booking/1";

        private readonly APIClient client;
        private readonly CompareHelper compare;

        private readonly Booking booking;
        private readonly BookingResponse bookingResponse;

        public UpdateBookingTests()
        {
            client = new APIClient();
            compare = new CompareHelper();
            
            // Set the API endpoint and request method;
            client.Url = APIurl;
            client.Method = "PATCH";
            
            // Set the Header information;
            client.AddHeader("Content-type", "application/json");
            client.AddHeader("Accept", "application/json");
            client.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            // Create booking object;
            booking = new Booking();
            booking.FirstName = "John";
            booking.LastName = "Doe";
            booking.TotalPrice = 1110;
            booking.DepositPaid = true;
            booking.BookingDates.Checkin = "2020-01-01";
            booking.BookingDates.Checkout = "2021-01-01";
            booking.AdditionalNeeds = "Breakfest";
        }

        [Fact]
        public void UpdateBooking_Auth_PUT()
        {
            // Add the Json Body;
            client.AddJsonBody(booking);

            // Execute the API request;
            client.Execute();

            // Verification;
            Assert.Equal(200, client.ResponseStatusCode);
            Assert.Equal("OK", client.ResponseStatusMsg);
            Assert.True(compare.CompareTwoJObjects(booking, client.ResponseContent));
        }

    }
}
