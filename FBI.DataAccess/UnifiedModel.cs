using System;
using System.Collections.Generic;
using System.Text;

namespace FBI.DataAccess
{
    public class UnifiedModel
    {
        public MostWantedProfilesModel.Item2 Item2 { get; set; }
        public ReportModel Report { get; set; }
        public List<ReportModel> Reports { get; set; }
    }
}
