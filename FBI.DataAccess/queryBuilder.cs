using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using static FBI.DataAccess.MostWantedProfilesModel;

namespace FBI.DataAccess
{
    public class queryBuilder
    {
        public NpgsqlCommand FillCommand(Item item, NpgsqlConnection con)
        {

            string images = string.Join(",", item.images.Select(img => img));
            string locations = "";

            if (item.locations != null)
            {
                locations = string.Join(",", item.locations.Where(loc => string.IsNullOrEmpty(loc) == true && loc != "null"));
            }

            var str = $@"INSERT INTO item (uid, title,description,images,caution,reward_max,locations,status,nationality,reward_min) 
                                VALUES (@uid,@title,@description,@images,@caution,@reward_max, @locations,@status,@nationality,@reward_min)
ON CONFLICT (uid) DO NOTHING";

            var dataFormat = new dataFormatHandler();

            NpgsqlCommand cmd = new NpgsqlCommand
            {
                CommandText = str,
                Connection = con,
                Parameters =
                        {
                            new NpgsqlParameter() { ParameterName = "uid", Value = item.uid},
                            new NpgsqlParameter() { ParameterName = "title", Value = item.title},
                            new NpgsqlParameter() { ParameterName = "description", Value = item.description},
                            new NpgsqlParameter() { ParameterName = "images", Value = item.images.Select(x => x.large).ToList()},
                            new NpgsqlParameter() { ParameterName = "caution", Value = dataFormat.stringIsNull(item.warning_message)},
                            new NpgsqlParameter() { ParameterName = "reward_max", Value = item.reward_max},
                            new NpgsqlParameter() { ParameterName = "locations", Value = locations.ToList()},
                            new NpgsqlParameter() { ParameterName = "status", Value = item.status},
                            new NpgsqlParameter() { ParameterName = "nationality", Value = dataFormat.stringIsNull(item.nationality)},
                            new NpgsqlParameter() { ParameterName = "reward_min", Value = item.reward_min}
                        }
            };

            return cmd;

        }

        public NpgsqlCommand addProfile(Item item,Image image, NpgsqlConnection con)
        {
            var dataFormat = new dataFormatHandler();
            var str = $"INSERT INTO item (title, uid, nationality, images, reward_max, description, caution, custom)" +
                $"VALUES (@title,@uid,@nationality,@images,@reward_max,@description,@caution, true)";

           var uid =  Guid.NewGuid().ToString("N");

            NpgsqlCommand cmd = new NpgsqlCommand
            {
                CommandText = str,
                Connection = con,
                Parameters =
                        {   
                            new NpgsqlParameter() { ParameterName = "uid", Value = uid},
                            new NpgsqlParameter() { ParameterName = "title", Value = item.title},
                            new NpgsqlParameter() { ParameterName = "description", Value = item.description},
                            new NpgsqlParameter() { ParameterName = "images", Value = new List<string> {image.large } },
                            new NpgsqlParameter() { ParameterName = "caution", Value = dataFormat.stringIsNull(item.warning_message)},
                            new NpgsqlParameter() { ParameterName = "reward_max", Value = item.reward_max},
                            new NpgsqlParameter() { ParameterName = "nationality", Value = dataFormat.stringIsNull(item.nationality)}
                        }
            };

            return cmd;


        }
        /////////////////////////////////////////////////////////////////////////////////////////
        public NpgsqlCommand SightingReportAdd(SightingReport SightingReport, NpgsqlConnection con)
        {
            var dataFormat = new dataFormatHandler();
            var str = $"INSERT INTO sightings (uid, time, date, addr, addrspect, comment)" +
                $"VALUES (@uid,@time,@data,@addr,@addrspect,@coment)";
            NpgsqlCommand cmd = new NpgsqlCommand
            {
                CommandText = str,
                Connection = con,
                Parameters =
                        {
                            new NpgsqlParameter() { ParameterName = "uid", Value = SightingReport.uid},
                            new NpgsqlParameter() { ParameterName = "time", Value = SightingReport.time},
                            new NpgsqlParameter() { ParameterName = "date", Value = SightingReport.date},
                            new NpgsqlParameter() { ParameterName = "addr", Value = SightingReport.addr},
                            new NpgsqlParameter() { ParameterName = "addrspect", Value = SightingReport.addrspect},
                            new NpgsqlParameter() { ParameterName = "comment", Value = SightingReport.comment}
                        }
            };

            return cmd;

        }

        public NpgsqlCommand Nuke(NpgsqlConnection con)
        {
            
            var str = $"DELETE FROM item";

            NpgsqlCommand cmd = new NpgsqlCommand()
            {
                CommandText = str,
                Connection = con
            };


            return cmd;



        }

        public NpgsqlCommand AddReport(NpgsqlConnection con, ReportModel report)
        {
            var str = $"INSERT INTO sightings (uid, time, date, addr, addrspec, comment )" +
                $"VALUES (@uid, @time, @date, @addr, @addrspec, @comment)";
            NpgsqlCommand cmd = new NpgsqlCommand
            {
                CommandText = str,
                Connection = con,
                Parameters =
                        {
                            new NpgsqlParameter() { ParameterName = "uid", Value = report.uid},
                            new NpgsqlParameter() { ParameterName = "time", Value = report.time},
                            new NpgsqlParameter() { ParameterName = "date", Value = report.date},
                            new NpgsqlParameter() { ParameterName = "addr", Value =  report.addr},
                            new NpgsqlParameter() { ParameterName = "addrspec", Value = report.addrspec},
                            new NpgsqlParameter() { ParameterName = "comment", Value = report.comment},
                        }
            };

            return cmd;
        }

        public NpgsqlCommand deleteReport(NpgsqlConnection con, int sid)
        {
            var str = "DELETE FROM sightings WHERE sid = @sid";

            NpgsqlCommand cmd = new NpgsqlCommand()
            {
                CommandText = str,
                Connection = con,
                Parameters =
                {
                    new NpgsqlParameter() {ParameterName = "sid", Value = sid}
                }
            };

            return cmd;
        }

        public NpgsqlCommand updateCommand(NpgsqlConnection con)
        {

            var str = $"DELETE FROM item WHERE custom = false";

            NpgsqlCommand cmd = new NpgsqlCommand()
            {
                CommandText = str,
                Connection = con
            };

            return cmd;

        }

        public NpgsqlCommand verifyReport(NpgsqlConnection con, int sid)
        {

            var str = $"UPDATE sightings SET verified = true WHERE sid =  @sid ";

            NpgsqlCommand cmd = new NpgsqlCommand()
            {
                CommandText = str,
                Connection = con,
                Parameters =
                {
                    new NpgsqlParameter() { ParameterName = "sid", Value = sid}
                }
            };

            return cmd;

        }

        //for testing loading and editing profiles only
        public NpgsqlCommand QueryOneRecordRandomly(NpgsqlConnection con, string uid)
        {

            var str = $"SELECT * FROM items WHERE uid = @uid";

            NpgsqlCommand cmd = new NpgsqlCommand
            {
                CommandText = str,
                Connection = con,
                Parameters =
                        {
                            new NpgsqlParameter() { ParameterName = "uid", Value = uid},
                           
                        }
            };

            return cmd;

        }

        public NpgsqlCommand GetFromDB(NpgsqlConnection con)
        {

            var str = $"SELECT * FROM item";

            NpgsqlCommand cmd = new NpgsqlCommand()
            {
                CommandText = str,
                Connection = con
            };

            return cmd;
        }

        public NpgsqlCommand UpdateOneEditedRecord(NpgsqlConnection con, Item2 item)
        {
            var dataFormat = new dataFormatHandler();
            var str = $@"UPDATE item set uid = @uid, title = @title, description = @description, images = @images, caution = @caution, reward_max = @reward_max, locations = @locations,
                      status = @status, nationality = @nationality, reward_min = @reward_min WHERE uid = @uid";


            NpgsqlCommand cmd = new NpgsqlCommand
            {
                CommandText = str,
                Connection = con,
                Parameters =
                        {
                            new NpgsqlParameter() { ParameterName = "uid", Value = item.uid},
                            new NpgsqlParameter() { ParameterName = "title", Value = item.title},
                            new NpgsqlParameter() { ParameterName = "description", Value = item.description},
                            new NpgsqlParameter() { ParameterName = "images", Value = item.images.Select(x => x).ToList()},
                            new NpgsqlParameter() { ParameterName = "caution", Value = dataFormat.stringIsNull(item.caution)},
                            new NpgsqlParameter() { ParameterName = "reward_max", Value = item.reward_max},
                            new NpgsqlParameter() { ParameterName = "locations", Value = item.locations},
                            new NpgsqlParameter() { ParameterName = "status", Value = item.status},
                            new NpgsqlParameter() { ParameterName = "nationality", Value = dataFormat.stringIsNull(item.nationality)},
                            new NpgsqlParameter() { ParameterName = "reward_min", Value = item.reward_min}
                        }
            };

            return cmd;
           }
    }
}
