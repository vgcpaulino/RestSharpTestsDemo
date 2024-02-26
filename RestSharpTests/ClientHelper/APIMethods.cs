using RestSharp;

namespace RestSharpTests.ClientHelper
{
    static class APIMethods
    {
        public static Method GetMethodObj(string method)
        {
            method = method.ToUpper();

            Dictionary<string, Method> methodsDictionary = new Dictionary<string, Method>
            {
                { "GET", Method.Get },
                { "POST", Method.Post },
                { "PATCH", Method.Patch },
                { "DELETE", Method.Delete },
                { "PUT", Method.Put },
                { "COPY", Method.Copy },
                { "OPTIONS", Method.Options },
                { "HEAD", Method.Head }
            };

            return methodsDictionary[method];
        }
    }
}