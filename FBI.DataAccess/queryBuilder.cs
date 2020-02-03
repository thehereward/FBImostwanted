﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBI.Webpage.Models;
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
                                VALUES (@uid,@title,@description,@images,@caution,@reward_max, @locations,@status,@nationality,@reward_min)";

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
                            new NpgsqlParameter() { ParameterName = "images", Value = item.images.Select(x => x).ToList()},
                            new NpgsqlParameter() { ParameterName = "caution", Value = dataFormat.stringIsNull(item.caution)},
                            new NpgsqlParameter() { ParameterName = "reward_max", Value = item.reward_max},
                            new NpgsqlParameter() { ParameterName = "locations", Value = locations.ToList()},
                            new NpgsqlParameter() { ParameterName = "status", Value = item.status},
                            new NpgsqlParameter() { ParameterName = "nationality", Value = dataFormat.stringIsNull(item.nationality)},
                            new NpgsqlParameter() { ParameterName = "reward_min", Value = item.reward_min}
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

        //for testing loading and editing profiles only
        public NpgsqlCommand QueryOneRecordRandomly(NpgsqlConnection con)
        {

            var str = $"SELECT * FROM items ORDER BY random() LIMIT 1";

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
