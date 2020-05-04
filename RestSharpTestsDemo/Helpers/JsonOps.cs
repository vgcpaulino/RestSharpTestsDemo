using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestSharpTestsDemo.Helpers
{
    public class JsonOps
    {

        public JsonOps()
        {  
        }

        public dynamic ConvertStrToJson(string jsonString)
        {
            return JsonConvert.DeserializeObject(jsonString);
        }

        public string ConvertObjToJson(dynamic jsonObj)
        {
            string result = JsonConvert.SerializeObject(jsonObj);
            return result;
        } 

        public JObject ConvertJsonStrToJObject(dynamic json)
        {
            return JObject.Parse(ConvertObjToJson(json));
        }

    }
}
