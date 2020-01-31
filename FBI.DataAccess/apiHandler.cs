﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FBI.DataAccess
{
    class apiHandler
    {
        public Root Root(int page)
        {
            var json = new WebClient().DownloadString($"https://api.fbi.gov/@wanted?page={page}");
            return JsonConvert.DeserializeObject<Root>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
