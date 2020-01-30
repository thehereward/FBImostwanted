using System;
using System.Collections.Generic;
using System.Text;

namespace FBI.DataAccess
{
    class dataFormatHandler
    {
        public string stringIsNull(string input)
        {
            if (input == null)
            {
                return "null";
            }

            return "input";
        }
    }
}
