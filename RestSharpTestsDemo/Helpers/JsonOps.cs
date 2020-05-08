using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestSharpTestsDemo.Helpers
{
    public class JsonOps
    {

        public JsonOps()
        {  
        }
        
        public JObject ObjToJObject(object obj)
        {
            string objString = ObjToString(obj);
            return StrToJObject(objString);
        }
        
        public string ObjToString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        
        public JObject StrToJObject(string str)
        {
            return JObject.Parse(str);
        }

        public JObject MergeJsonObject(JObject receiverObj, JObject toMergeObj)
        {
            receiverObj.Merge(toMergeObj, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });
            return receiverObj;
        }

        public bool CompareRequestAndResponseJson(JObject requestJsonObj, JObject responseJsonObj)
        {
            if (!JObjectHasEqualCount(requestJsonObj, responseJsonObj))
            {
                return false;
            }

            foreach (JProperty prop in requestJsonObj.Properties())
            {
                dynamic responseValue = (dynamic)responseJsonObj[prop.Name];
                dynamic requestValue = (dynamic)requestJsonObj[prop.Name];
                if (!(responseValue.Value == requestValue.Value))
                {
                    return false;
                }
            }

            return true;
        }

        public bool JObjectHasEqualCount(JObject requestJsonObj, JObject responseJsonObj)
        {
            return (requestJsonObj.Count == responseJsonObj.Count);
        }

    }
}
