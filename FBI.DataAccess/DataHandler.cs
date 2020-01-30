using Newtonsoft.Json;
using Npgsql;
using System.Linq;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Collections.Generic;

namespace FBI.DataAccess
{

    public class DataHandler
    {
        string cs = "Host=localhost;Username=postgres;Password=password;Database=FBImostwanted";

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
                    NpgsqlCommand cmd = queryBuilder.FillCommand(fugitive,con);

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
        public void addProfile(Item item)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var queryBuilder = new queryBuilder();
                var cmd = queryBuilder.addProfile(item, con);

                cmd.ExecuteNonQuery();

            }
        }
    }
}
