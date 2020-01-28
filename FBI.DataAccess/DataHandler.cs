using FBI.Webpage.Models;
using Newtonsoft.Json;
using System;
using System.Net;

namespace FBI.DataAccess
{
    public class DataHandler
    {
        public Root Root() {
            var json = new WebClient().DownloadString("https://api.fbi.gov/@wanted");
            return JsonConvert.DeserializeObject<Root>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
