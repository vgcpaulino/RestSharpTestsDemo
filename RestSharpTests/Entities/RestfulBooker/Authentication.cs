using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpTests.Entities.RestfulBooker
{
    public class Authentication
    {

        [JsonProperty(PropertyName = "username")] 
        public string Username { get; internal set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; internal set; }
   
    }
}
