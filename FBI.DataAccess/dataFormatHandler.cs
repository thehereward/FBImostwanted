using System;
using System.Collections.Generic;
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
    }
}
