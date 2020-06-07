using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestSharpTests.Helpers
{
    public class ConversionHelper
    {

        public JObject JTokenToJObject(JToken token)
        {
            return StringToJObject(token.ToString());
        }

        public JObject StringToJObject(string jsonString)
        {
            return JObject.Parse(jsonString);
        }

        public JArray StringToJArray(string jsonString)
        {
            return JArray.Parse(jsonString);
        }

        public string ObjToString(object obj, bool ignoreDefaultValues = false)
        {
            JsonSerializerSettings serializer = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                Formatting = Formatting.Indented
            };

            if (ignoreDefaultValues)
            {
                serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
            }

            return JsonConvert.SerializeObject(obj, serializer);
        }

        public JObject ObjToJObject(object obj, bool ignoreDefaultValues = false)
        {
            string jsonString = ObjToString(obj, ignoreDefaultValues);
            return StringToJObject(jsonString);
        }

        public JObject MergeJObject(JObject receiverObj, JObject toMergeObj)
        {
            receiverObj.Merge(toMergeObj, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });
            return receiverObj;
        }
    }
}
