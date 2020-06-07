using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace RestSharpTests.Helpers
{
    public class CompareHelper
    {
        private readonly ConversionHelper conversion;

        public CompareHelper() 
        {
            conversion = new ConversionHelper();
        }

        /*public bool CompareTwoJObjects(object expectedObj, string actualStr, bool ignoreCount = false)
        {
            JObject expectedJObj = conversion.ObjToJObject(expectedObj);
            JObject actualJObj = conversion.StringToJObject(actualStr);

            return ExecuteComparsion(expectedJObj, actualJObj, ignoreCount);
        }*/
        
        public bool CompareTwoJObjects(object expectedObj, string actualStr, bool ignoreCount = false, List<string> ingoredFields = null)
        {
            JObject expectedJObj = conversion.ObjToJObject(expectedObj);
            JObject actualJObj = conversion.StringToJObject(actualStr);

            return ExecuteComparsion(expectedJObj, actualJObj, ignoreCount, ingoredFields);
        }

        public bool CompareTwoJObjects(string expectedStr, string actualStr, bool ignoreCount = false, List<string> ingoredFields = null)
        {
            JObject expectedJObj = conversion.StringToJObject(expectedStr);
            JObject actualJObj = conversion.StringToJObject(actualStr);

            return ExecuteComparsion(expectedJObj, actualJObj, ignoreCount, ingoredFields);
        }


        private bool ExecuteComparsion(JObject expectedObj, JObject actualObj, bool ignoreCount = false, List<string> ingoredFields = null)
        {
            if (!ignoreCount)
            {
                if (!JObjectHasEqualCount(expectedObj, actualObj))
                {
                    return false;
                }
            }

            foreach (JProperty prop in expectedObj.Properties())
            {
                // Check if the Property name must be ignored;
                if (ingoredFields != null)
                {
                    bool ignoreThisProperty = ingoredFields.Contains(prop.Name);
                    if (ignoreThisProperty)
                    {
                        continue;
                    }
                }

                //Get the values from the property for both objects;
                dynamic excpectedValue = (dynamic)expectedObj[prop.Name];
                dynamic actualValue = (dynamic)actualObj[prop.Name];

                // Compare both values;                    
                if (!(excpectedValue.Value == actualValue.Value))
                {
                    string output = "Property: " + prop.Name + " with different value."
                        + "\n"
                        + "Expected Obj = " + excpectedValue
                        + "\n"
                        + "Actual Obj Value = " + actualValue;
                    return false;
                }
            }

            return true;
        }
        
        private bool JObjectHasEqualCount(JObject expectedObj, JObject actualObj)
        {
            return (expectedObj.Count == actualObj.Count);
        }
    }
}
