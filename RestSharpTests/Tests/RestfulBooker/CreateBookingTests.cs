using RestSharpTests.ClientHelper;
using RestSharpTests.Entities.RestfulBooker;
using RestSharpTests.Helpers;
using System.Collections.Generic;
using Xunit;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class CreateBookingTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/booking";
        private readonly APIClient client;
        private readonly CompareHelper compare;
        private readonly ResponseSearch responseSearch;

        private readonly Booking booking;
        private readonly BookingResponse bookingResponse;      

        public CreateBookingTests()
        {
            client = new APIClient();
            compare = new CompareHelper();
            responseSearch = new ResponseSearch();

            // Set the API endpoint and request method;
            client.Url = APIurl;
            client.Method = "POST";

            // Set the Header information;
            client.AddHeader("Content-Type", "application/json");

            // Create booking object;
            booking = new Booking();
            booking.FirstName = "John";
            booking.LastName = "Doe";
            booking.TotalPrice = 1110;
            booking.DepositPaid = true;
            booking.BookingDates.Checkin = "2020-01-01";
            booking.BookingDates.Checkout = "2021-01-01";
            booking.AdditionalNeeds = "Breakfest";

            // Create booking response object;
            bookingResponse = new BookingResponse();
            bookingResponse.BookingId = 0;
            bookingResponse.Booking = booking;
        }

        [Fact]
        public void CreateBooking_RequireAccept_Post()
        {
            // Add the Json Body;
            client.AddJsonBody(booking);

            // Execute the API request;
            client.Execute();

            // Verification;
            Assert.Equal(418, client.ResponseStatusCode);
            Assert.Equal("I'm a teapot", client.ResponseStatusMsg);
            Assert.Equal("I'm a teapot", client.ResponseContent);
        }

        [Fact]
        public void CreateBooking_Post() {
            // Add the Header parameters;
            client.AddHeader("Accept", "*/*");

            // Add the Json Body;
            client.AddJsonBody(booking);

            // Execute the API request;
            client.Execute();

            // Verification;
            Assert.Equal(200, client.ResponseStatusCode);
            Assert.Equal("OK", client.ResponseStatusMsg);
            string bookingid = responseSearch.GetPropertyValue(client.ResponseContent, "bookingid");
            Assert.NotNull(bookingid);

            // AJUSTAR PARA IGNORAR ALGUNS ITENS COMO BOOKINGID
            List<string> ignoredFields = new List<string>() { "bookingid" };
            Assert.True(compare.CompareTwoJObjects(bookingResponse, client.ResponseContent, false, ignoredFields));
        }
        
    }
}
