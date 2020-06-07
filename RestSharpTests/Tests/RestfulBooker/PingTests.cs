using RestSharpTests.ClientHelper;
using Xunit;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class PingTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/ping";
        private readonly APIClient client;

        public PingTests()
        {
            client = new APIClient();
        }

        [Fact]
        public void Ping()
        {
            // Set the API endpoint and request method;
            client.Url = APIurl;
            client.Method = "GET";
            
            // Call the API;
            client.Execute();

            // Verification;
            Assert.Equal(201, client.ResponseStatusCode);
            Assert.Equal("Created", client.ResponseStatusMsg);
            Assert.Equal("Created", client.ResponseContent);
        }

    }
}
