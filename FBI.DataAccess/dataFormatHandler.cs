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
                return "";
            }

            return input;
        }
    }
}
