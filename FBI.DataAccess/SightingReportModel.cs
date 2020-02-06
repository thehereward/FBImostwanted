using System;
using System.Collections.Generic;
using System.Text;

namespace FBI.DataAccess
{
    public class SightingReport
    {
        public string sid { get; set; }
        public string uid { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string addr { get; set; }
        public string addrspect { get; set; }
        public string comment { get; set; }
        public bool verified { get; set; }

    
    
        public List<SightingReport> SightingReportsList { get; set; }

    }
}
