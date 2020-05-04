using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;
using RestSharpTestsDemo.Helpers;

namespace RestSharpTestsDemo.RestfulBooker
{
    public class UpdateBookingTests
    {

        private const string APIurl = "https://restful-booker.herokuapp.com/booking/1";
        private readonly RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;
        private string responseBody;
        private int numericStatusCode;
        private readonly ResponseParser parser;
        private readonly JsonOps jsonOps;
        private readonly object jsonBody;
        private readonly string stringBody;


        public UpdateBookingTests()
        {
            // Set the base URL;
            restClient = new RestClient($"{APIurl}");

            parser = new ResponseParser();
            jsonOps = new JsonOps();

            jsonBody = new
            {
                firstname = "John",
                lastname = "Doe",
                totalprice = "1110",
                depositpaid = "true",
                bookingdates = new
                {
                    checkin = "2020-01- 01",
                    checkout = "2021-01-01"
                },
                additionalneeds = "Breakfast"
            };
            stringBody = jsonOps.ConvertObjToJson(jsonBody);
        }

        [Test]
        public void UpdateBooking_Auth_PUT()
        {
            // Set the Request Method;
            restRequest = new RestRequest(Method.PUT);

            // Set the Header info;
            restRequest.AddHeader("Content-type", "application/json");
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            // Set the Body info;
            restRequest.AddParameter("application/json,text/plain", stringBody, ParameterType.RequestBody);

            // Call the API;
            restResponse = restClient.Execute(restRequest);

            // Get the "Body" content and "Status Code";
            responseBody = restResponse.Content;
            numericStatusCode = parser.GetStatusCode(restResponse);

            // Verify the "Body" and "Status Code";
            var requestJsonObj = jsonOps.ConvertJsonStrToJObject(jsonBody);
            var responseJsonObj = jsonOps.ConvertStrToJson(restResponse.Content);
            Assert.True(CompareRequestAndResponseJson(requestJsonObj, responseJsonObj));
            Assert.AreEqual(200, numericStatusCode);
        }

        private bool CompareRequestAndResponseJson(JObject requestJsonObj, JObject responseJsonObj)
        {
            string responseFirstName = (string)responseJsonObj["firstname"];
            string responseLastName = (string)responseJsonObj["lastname"];
            string responseTotalPrice = (string)responseJsonObj["totalprice"];
            string responseDepositPaid = (string)responseJsonObj["depositpaid"];
            string responseAdditionalNeeds = (string)responseJsonObj["additionalneeds"];

            string requestFirstName = (string)requestJsonObj["firstname"];
            string requestLastName = (string)responseJsonObj["lastname"];
            string requestTotalPrice = (string)responseJsonObj["totalprice"];
            string requestDepositPaid = (string)responseJsonObj["depositpaid"];
            string requestAdditionalNeeds = (string)responseJsonObj["additionalneeds"];

            return responseFirstName == requestFirstName
                && responseLastName == requestLastName
                && responseTotalPrice == requestTotalPrice
                && responseDepositPaid == requestDepositPaid
                && responseAdditionalNeeds == requestAdditionalNeeds;
        }

    }
}
