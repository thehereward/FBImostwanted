using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FBI.DataAccess
{
    public class dataFormatHandler
    {
        public string stringIsNull(string input)
        {
            if (input == null)
            {
                return "null";
            }

            return input;
        }

        public string cautionRemove(string input)
        {
            if (input.Contains("SHOULD BE CONSIDERED "))
            {
                input = input.Remove(0, 21);
            }

            return input;
        }

        public ReportModel RemoveNulls(ReportModel myObject)
        {

        foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                var item = pi.GetValue(myObject, null);
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(myObject);
                    if (string.IsNullOrEmpty(value))
                    {
                        pi.SetValue(myObject,"null");
                    }
                }
            }
            return myObject;
        }

    }
}
