using RestSharpTests.ClientHelper;
using RestSharpTests.Entities.RestfulBooker;
using RestSharpTests.Helpers;
using RestSharpTestsDemo.RestfulBooker;
using Xunit;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class PartialUpdateBookingTests : BaseTests
    {
        private readonly DataGenerator testData;

        public PartialUpdateBookingTests()
        {
            testData = new DataGenerator();
            
            // Test Info;
            HttpClient.Url = TestUrl.BookingWithId(testData.BookingId);
            HttpClient.Method = "PATCH";

            HttpClient.AddHeader("Accept", "application/json");
            HttpClient.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            BookingModel.AdditionalNeeds = "Dinner";
        }

        [Fact]
        public void UpdateBooking_Auth_UsingAddJsonBodyMethod_PATCH()
        {
            HttpClient.AddJsonBody(BookingModel);

            HttpClient.Execute();

            Assert.Equal(200, HttpClient.ResponseStatusCode);
            Assert.Equal("OK", HttpClient.ResponseStatusMsg);

            // Creating the expected BookingModel object;
            Booking expectedBookingObj = testData.bookingModel;
            expectedBookingObj.AdditionalNeeds = BookingModel.AdditionalNeeds;

            bool rightResponseJson = Compare.CompareTwoJObjects(expectedBookingObj, HttpClient.ResponseContent);
            Assert.True(rightResponseJson);
        }

    }
}
