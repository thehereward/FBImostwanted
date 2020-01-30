using FBI.Webpage.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FBI.DataAccess
{
    class apiHandler
    {
        public Root Root()
        {
            var json = new WebClient().DownloadString("https://api.fbi.gov/@wanted");
            return JsonConvert.DeserializeObject<Root>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
