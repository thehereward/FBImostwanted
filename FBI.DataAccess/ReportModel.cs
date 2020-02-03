using System;
using System.Collections.Generic;
using System.Text;

namespace FBI.DataAccess
{
    public class ReportModel
    {

        public int sid { get; set; }
        public string uid { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string addr { get; set; }
        public string addrspec { get; set; }
        public string comment { get; set; }
    }
}
