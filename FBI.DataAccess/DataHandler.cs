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
        string cs = "Host=localhost;Username=postgres;Password=password;Database=FBImostwanted";

        public Root Root()
        {
            var json = new WebClient().DownloadString("https://api.fbi.gov/@wanted");
            return JsonConvert.DeserializeObject<Root>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public void AddtoDB(Root root)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();

                foreach (var item in root.items)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand();

                    string images = "";
                    foreach (var image in item.images)
                    {
                        if (images == "")
                        {
                            images = image.large;
                        }
                        else
                        {
                            images = images + "," + image.large;
                        }
                    }

                    string locations = "null";

                    if (item.locations != null)
                    {
                        foreach (var location in item.locations)
                        {
                            if (locations == "null")
                            {
                                locations = location;
                            }
                            else
                            {
                                locations = locations + "," + location;
                            }
                        }
                    }
                    var remove = "";
                    if (item.warning_message != null)
                    { remove = item.warning_message.Replace("'", ""); }

                    var str = $"INSERT INTO item (uid, title, description, images, caution,reward_max, locations,status,nationality,reward_min) VALUES ('{item.uid}','{item.title}','{item.description}','{{{images}}}','{remove}',{item.reward_max}, '{{{locations}}}','{item.status}','{item.nationality}',{item.reward_min})";
                    cmd.CommandText = str;

                    cmd.Connection = con;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {

                    }
                }
            }

        }

        public void SelfDestruct()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();

                var str = $"DELETE FROM item";

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = str;
                cmd.Connection = con;


                cmd.ExecuteNonQuery();

            }
        }
    }
}
