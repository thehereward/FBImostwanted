using FBI.Webpage.Models;
using Newtonsoft.Json;
using Npgsql;
using System.Linq;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Collections.Generic;
using Dapper;
using static FBI.DataAccess.MostWantedProfilesModel;

namespace FBI.DataAccess
{

    public class DataHandler
    {
        string cs = "Server=localhost;Port=5432;User Id=ISpy;Password=pass123;Database=ISpy;";

        public void FillDB()
        {

            var apiHandler = new apiHandler();
            var root = apiHandler.Root();

            foreach (var fugitive in root.items)
            {
                using (var con = new NpgsqlConnection(cs))
                {
                    con.Open();

                    var queryBuilder = new queryBuilder();
                    NpgsqlCommand cmd = queryBuilder.FillCommand(fugitive, con);

                    try
                    {

                        cmd.ExecuteNonQuery();

                    }
                    catch
                    {
                        //TODO: logging
                        throw;
                    }
                }
            }
        }

        public void SelfDestruct()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var queryBuilder = new queryBuilder();
                var cmd = queryBuilder.Nuke(con);

                cmd.ExecuteNonQuery();
            }
        }

        public void updateDB()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var queryBuilder = new queryBuilder();
                var cmd = queryBuilder.updateCommand(con);

                cmd.ExecuteNonQuery();
                FillDB();
            }

        }


        public Item2 SelctOneRecordRandomly()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();

                var querymaker = new queryBuilder();
                NpgsqlCommand cmd = querymaker.QueryOneRecordRandomly(con);

                List<Item2> itemsBFB = new List<Item2>();

                Root2 root = new Root2() { items = itemsBFB };
                root.items= (con.Query<Item2>($"SELECT * FROM item ORDER BY random() LIMIT 1").ToList());
                
            
                return root.items[0];
            }
        }

        public void UpdateAProfile(Item2 item)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();

                var querymaker = new queryBuilder();
                NpgsqlCommand cmd = querymaker.UpdateOneEditedRecord(con, item);
                cmd.ExecuteNonQuery();
            }

           

        }
    }
}
