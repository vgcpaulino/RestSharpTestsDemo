using Newtonsoft.Json;

namespace RestSharpTests.Entities.RestfulBooker
{
    public class BookingDates
    {

        [JsonProperty(PropertyName = "checkin")]
        public string Checkin { get; set; }

        [JsonProperty(PropertyName = "checkout")]
        public string Checkout { get; set; }

    }
}
