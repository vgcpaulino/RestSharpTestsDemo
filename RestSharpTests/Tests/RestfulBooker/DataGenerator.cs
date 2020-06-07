using RestSharpTests.ClientHelper;
using RestSharpTests.Entities.RestfulBooker;
using RestSharpTests.Helpers;
using RestSharpTests.Tests;

namespace RestSharpTestsDemo.RestfulBooker
{
    public class DataGenerator
    {
        private readonly ResponseSearch responseSearch;
        
        public Booking bookingModel;
        public int BookingId { get; internal set; }
        public string Token { get; internal set; }

        public DataGenerator()
        {
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

            bookingModel = new Booking();
            bookingModel.FirstName = "John";
            bookingModel.LastName = "Doe";
            bookingModel.TotalPrice = 1110;
            bookingModel.DepositPaid = true;
            bookingModel.BookingDates.Checkin = "2020-01-01";
            bookingModel.BookingDates.Checkout = "2021-01-01";
            bookingModel.AdditionalNeeds = "Breakfest";

            client.AddJsonBody(bookingModel);

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
