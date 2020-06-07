using Xunit;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class AuthenticationTests : BaseTests
    {

        public AuthenticationTests()
        {
            // Set the API endpoint and request method;
            HttpClient.Url = TestUrl.Auth;
            HttpClient.Method = "POST";

            // Set the Header information;
            HttpClient.AddHeader("Content-Type", "application/json");
        }

        [Fact]
        public void Authentication_RightCredentials_Post()
        {
            // Create object with credentials;
            AuthModel.Username = "admin";
            AuthModel.Password = "password123";
            
            // Add the Json Body;
            HttpClient.AddJsonBody(AuthModel);

            // Execute the API request;
            HttpClient.Execute();

            // Verification;
            Assert.Equal(200, HttpClient.ResponseStatusCode);
            Assert.Equal("OK", HttpClient.ResponseStatusMsg);
            string token = Search.GetPropertyValue(HttpClient.ResponseContent, "token");
            Assert.NotNull(token);
        }

        [Fact]
        public void Authentication_BadCredentials_Post()
        {
            // Create object with credentials;
            AuthModel.Username = "admin";
            AuthModel.Password = "password12";

            // Add the Json Body;
            HttpClient.AddJsonBody(AuthModel);

            // Execute the API request;
            HttpClient.Execute();

            // Verification;
            Assert.Equal(200, HttpClient.ResponseStatusCode);
            Assert.Equal("OK", HttpClient.ResponseStatusMsg);
            string reason = Search.GetPropertyValue(HttpClient.ResponseContent, "reason");
            Assert.Equal("Bad credentials", reason);
        }

        [Fact]
        public void Authentication_NoCredentials_Post()
        {
            // Execute the API request;
            HttpClient.Execute();

            // Verification;
            Assert.Equal(200, HttpClient.ResponseStatusCode);
            Assert.Equal("OK", HttpClient.ResponseStatusMsg);

            string reason = Search.GetPropertyValue(HttpClient.ResponseContent, "reason");
            Assert.Equal("Bad credentials", reason);
        }

    }
}
