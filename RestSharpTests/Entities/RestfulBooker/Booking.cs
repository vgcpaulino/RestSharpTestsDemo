using Newtonsoft.Json;

namespace RestSharpTests.Entities.RestfulBooker
{
    public class Booking
    {
        
        public Booking()
        {
            BookingDates = new BookingDates();
        }

        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; internal set; }
        
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; internal set; }
        
        [JsonProperty(PropertyName = "totalprice")]
        public double TotalPrice { get; internal set; }
        
        [JsonProperty(PropertyName = "depositpaid")]
        public bool DepositPaid { get; internal set; }

        [JsonProperty(PropertyName = "bookingdates")]
        public BookingDates BookingDates { get; internal set; }

        [JsonProperty(PropertyName = "additionalneeds")]
        public string AdditionalNeeds { get; internal set; }

    }

}
