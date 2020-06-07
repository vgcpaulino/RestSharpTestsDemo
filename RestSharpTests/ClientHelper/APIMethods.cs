using System;
using System.Collections.Generic;
using RestSharp;

namespace RestSharpTests.ClientHelper
{
    static class APIMethods
    {

        public static Method GetMethodObj(string method)
        {
            method = method.ToUpper();

            Dictionary<string, Method> methodsDictionary = new Dictionary<string, Method>();
            methodsDictionary.Add("GET", Method.GET);
            methodsDictionary.Add("POST", Method.POST);
            methodsDictionary.Add("PATCH", Method.PATCH);
            methodsDictionary.Add("DELETE", Method.DELETE);
            methodsDictionary.Add("PUT", Method.PUT);
            methodsDictionary.Add("COPY", Method.COPY);
            methodsDictionary.Add("OPTIONS", Method.OPTIONS);
            methodsDictionary.Add("HEAD", Method.HEAD);

            return methodsDictionary[method];
        }

    }
}
