using RestSharp;

namespace RestSharpTestsDemo.Helpers
{
    public class ResponseParser
    {

        public ResponseParser()
        {
        }

        public int GetStatusCode(IRestResponse restResponse)
        {
            return (int)restResponse.StatusCode;
        }

    }
}
