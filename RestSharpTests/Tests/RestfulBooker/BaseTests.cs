using RestSharpTests.ClientHelper;
using RestSharpTests.Entities.RestfulBooker;
using RestSharpTests.Helpers;
using RestSharpTests.Tests.RestfulBooker;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace RestSharpTests.Tests
{
    public class BaseTests
    {
        internal readonly APIClient HttpClient;
        internal readonly ResponseSearch Search;
        internal readonly CompareHelper Compare;

        internal readonly Authentication AuthModel;
        internal readonly Booking BookingModel;
        internal readonly BookingResponse BookingRespModel;

        internal readonly BaseTestsUrls TestUrl;

        public BaseTests()
        {
            HttpClient = new APIClient();
            Search = new ResponseSearch();
            Compare = new CompareHelper();

            AuthModel = new Authentication();
            BookingModel = new Booking();
            BookingRespModel = new BookingResponse();

            TestUrl = new BaseTestsUrls();
        }
    }

    public class BaseTestsUrls
    {
        public BaseTestsUrls()
        {
            string BaseUrl = "https://restful-booker.herokuapp.com";
            Auth = $"{BaseUrl}/auth";
            Booking = $"{BaseUrl}/booking";
            Ping = $"{BaseUrl}/ping";
        }
       
        public string Auth { get; private set; }
        public string Booking { get; private set; }
        public string Ping { get; private set; }

        public string BookingWithId(int bookingId)
        {
            return $"{Booking}/{bookingId}";
        }
    }
}
