using FBI.Webpage.Models;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace FBI.DataAccess
{
    public class DataHandler
    {


        public Root Root() {
            var json = new WebClient().DownloadString("https://api.fbi.gov/@wanted");
            return JsonConvert.DeserializeObject<Root>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public void Save(Root root)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\wanted.json");
            var dataToSave = JsonConvert.SerializeObject(root);
            System.IO.File.WriteAllText(path, dataToSave);
        }
    }
}
