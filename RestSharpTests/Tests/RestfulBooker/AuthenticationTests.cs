using Xunit;
using RestSharpTests.ClientHelper;
using RestSharpTests.Entities.RestfulBooker;
using RestSharpTests.Helpers;

namespace RestSharpTests.Tests.RestfulBooker
{
    public class AuthenticationTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/auth";
        private readonly APIClient client;
        private readonly Authentication auth;
        ResponseSearch responseSearch = new ResponseSearch();

        public AuthenticationTests()
        {
            client = new APIClient();
            auth = new Authentication();
            responseSearch = new ResponseSearch();

            // Set the API endpoint and request method;
            client.Url = APIurl;
            client.Method = "POST";

            // Set the Header information;
            client.AddHeader("Content-Type", "application/json");
        }

        [Fact]
        public void Authentication_RightCredentials_Post()
        {
            // Create object with credentials;
            auth.Username = "admin";
            auth.Password = "password123";
            
            // Add the Json Body;
            client.AddJsonBody(auth);

            // Execute the API request;
            client.Execute();

            // Verification;
            Assert.Equal(200, client.ResponseStatusCode);
            Assert.Equal("OK", client.ResponseStatusMsg);
            string token = responseSearch.GetPropertyValue(client.ResponseContent, "token");
            Assert.NotNull(token);
        }

        [Fact]
        public void Authentication_BadCredentials_Post()
        {
            // Create object with credentials;
            auth.Username = "admin";
            auth.Password = "password12";

            // Add the Json Body;
            client.AddJsonBody(auth);

            // Execute the API request;
            client.Execute();

            // Verification;
            Assert.Equal(200, client.ResponseStatusCode);
            Assert.Equal("OK", client.ResponseStatusMsg);
            string reason = responseSearch.GetPropertyValue(client.ResponseContent, "reason");
            Assert.Equal("Bad credentials", reason);
        }

        [Fact]
        public void Authentication_NoCredentials_Post()
        {
            // Execute the API request;
            client.Execute();

            // Verification;
            Assert.Equal(200, client.ResponseStatusCode);
            Assert.Equal("OK", client.ResponseStatusMsg);

            string reason = responseSearch.GetPropertyValue(client.ResponseContent, "reason");
            Assert.Equal("Bad credentials", reason);
        }

    }
}
