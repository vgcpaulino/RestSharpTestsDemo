using RestSharpTests.ClientHelper;
using RestSharpTests.Entities.RestfulBooker;
using RestSharpTests.Helpers;
using RestSharpTestsDemo.RestfulBooker;
using Xunit;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class PartialUpdateBookingTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/booking";
        private readonly APIClient client;
        private Booking booking;
        private Booking expectedBookingObj;
        private readonly DataGenerator testData;

        private readonly CompareHelper compare;

        public PartialUpdateBookingTests()
        {
            client = new APIClient();
            testData = new DataGenerator();
            compare = new CompareHelper();


            client.Url = $"{APIurl}/{testData.BookingId}";
            client.Method = "PATCH";

            client.AddHeader("Accept", "application/json");
            client.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            booking = new Booking();
            booking.AdditionalNeeds = "Dinner";

        }

        [Fact]
        public void UpdateBooking_Auth_UsingAddJsonBodyMethod_PATCH()
        {
            client.AddJsonBody(booking);

            client.Execute();

            Assert.Equal(200, client.ResponseStatusCode);
            Assert.Equal("OK", client.ResponseStatusMsg);

            // Creating the expected booking object;
            expectedBookingObj = testData.booking;
            expectedBookingObj.AdditionalNeeds = booking.AdditionalNeeds;

            bool rightResponseJson = compare.CompareTwoJObjects(expectedBookingObj, client.ResponseContent);
            Assert.True(rightResponseJson);
        }

    }
}
