using RestSharpTests.ClientHelper;
using RestSharpTests.Entities.RestfulBooker;
using RestSharpTests.Helpers;

namespace RestSharpTestsDemo.RestfulBooker
{
    public class DataGenerator
    {

        private readonly APIClient client;        
        private readonly ResponseSearch responseSearch;
        
        public Booking booking;
        public int BookingId { get; internal set; }
        public string Token { get; internal set; }

        public DataGenerator()
        {
            client = new APIClient();
            responseSearch = new ResponseSearch();

            Token = GetTokenId();
            BookingId = GetNewBooking();
        }

        private int GetNewBooking()
        {
            APIClient client = new APIClient();

            client.Url = "https://restful-booker.herokuapp.com/booking";

            client.Method = "POST";

            client.AddHeader("Content-Type", "application/json");
            client.AddHeader("Accept", "*/*");

            booking = new Booking();
            booking.FirstName = "John";
            booking.LastName = "Doe";
            booking.TotalPrice = 1110;
            booking.DepositPaid = true;
            booking.BookingDates.Checkin = "2020-01-01";
            booking.BookingDates.Checkout = "2021-01-01";
            booking.AdditionalNeeds = "Breakfest";

            client.AddJsonBody(booking);

            client.Execute();

            string bookingIdStr = responseSearch.GetPropertyValue(client.ResponseContent, "bookingid");

            return int.Parse(bookingIdStr);
        }

        private string GetTokenId()
        {
            APIClient client = new APIClient();

            client.Url = "https://restful-booker.herokuapp.com/auth";

            client.Method = "POST";

            client.AddHeader("Content-Type", "application/json");

            Authentication auth = new Authentication();
            auth.Username = "admin";
            auth.Password = "password123";

            client.AddJsonBody(auth);

            client.Execute();

            return responseSearch.GetPropertyValue(client.ResponseContent, "token");
        }

    }
}
