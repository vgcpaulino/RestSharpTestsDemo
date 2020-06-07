using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpTests.Entities.RestfulBooker
{
    public class BookingResponse
    {

        [JsonProperty(PropertyName = "bookingid")]
        public int BookingId { get; internal set; }

        [JsonProperty(PropertyName = "booking")] 
        public Booking Booking { get; internal set; }
    }
}
