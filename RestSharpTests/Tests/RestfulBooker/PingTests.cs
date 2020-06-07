using RestSharpTests.ClientHelper;
using Xunit;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class PingTests : BaseTests
    {
        public PingTests()
        {
            // Set the API endpoint and request method;
            HttpClient.Url = TestUrl.Ping;
            HttpClient.Method = "GET";
        }

        [Fact]
        public void Ping()
        {
            // Call the API;
            HttpClient.Execute();

            // Verification;
            Assert.Equal(201, HttpClient.ResponseStatusCode);
            Assert.Equal("Created", HttpClient.ResponseStatusMsg);
            Assert.Equal("Created", HttpClient.ResponseContent);
        }

    }
}
