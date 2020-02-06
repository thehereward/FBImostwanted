using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FBI.DataAccess
{
    public class apiHandler
    {
        public Root Root(int page)
        {
            var json = new WebClient().DownloadString($"https://api.fbi.gov/@wanted?page={page}");
            return JsonConvert.DeserializeObject<Root>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public Location googleapi(string postCode, string key)
        {
            var json = new WebClient().DownloadString($"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={postCode}&key={key}");
            return JsonConvert.DeserializeObject<Location>(json);
        }
    }

    public class Prediction
    {
        public string description { get; set; }
    }

    public class Location
    {
        public List<Prediction> predictions { get; set; }

    }
}
