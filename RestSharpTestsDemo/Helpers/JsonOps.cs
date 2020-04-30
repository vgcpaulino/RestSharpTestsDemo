using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
    }
}
