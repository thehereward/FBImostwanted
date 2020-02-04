using Newtonsoft.Json;
using Npgsql;
using System.Linq;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Collections.Generic;
using static FBI.DataAccess.MostWantedProfilesModel;
using Dapper;

namespace FBI.DataAccess
{

    public class DataHandler
    {
        string cs = "";

        public void FillDB()
        {
            for (var i = 1; i <= 50; i++)
            {
                var apiHandler = new apiHandler();
                var root = apiHandler.Root(i);

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

        public void addProfile(Item item, Image image)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var queryBuilder = new queryBuilder();
                var cmd = queryBuilder.addProfile(item, image, con);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
        }
        public Root2 GetFromDB()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var queryBuilder = new queryBuilder();
                var cmd = queryBuilder.Index(con);

                List<Item2> Fugitives = new List<Item2>();

                Root2 root = new Root2() { items = Fugitives };
                root.items = con.Query<Item2>($"SELECT * FROM item").ToList();
                Fugitives.OrderBy(attribute => attribute.custom == true);

                var dataformater = new dataFormatHandler();

                foreach(var item in root.items)
                {

                    item.caution = dataformater.cautionRemove(item.caution);
                }

                return root;
            }

        }

        public void ReportSighting(ReportModel report)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var queryBuilder = new queryBuilder();
                var cmd = queryBuilder.AddReport(con,report);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }

        }

        public List<ReportModel> reports(int uid)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var reports = new List<ReportModel>();
                reports = con.Query<ReportModel>($"SELECT * FROM sightings WHERE uid = @uid", uid).ToList();
                
                

                return reports;
            }
        }

        public void approveSighting(int report)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var queryBuilder = new queryBuilder();
                var cmd = queryBuilder.verifyReport(con, report);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
        }

        public void DeleteSighting(int report)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var queryBuilder = new queryBuilder();
                var cmd = queryBuilder.deleteReport(con, report);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }

            }
        }
        
        
    }
}
