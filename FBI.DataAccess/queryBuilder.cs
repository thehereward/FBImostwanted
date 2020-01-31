using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace FBI.DataAccess
{
    class queryBuilder
    {
        public NpgsqlCommand FillCommand(Item item, NpgsqlConnection con)
        {

            string images = string.Join(",", item.images.Select(img => img.large));
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

        public NpgsqlCommand Index(NpgsqlConnection con)
        {

            var str = $"SELECT * FROM item WHERE title = @title";

            NpgsqlCommand cmd = new NpgsqlCommand()
            {
                CommandText = str,
                Connection = con
            };


            return cmd;



        }
    }
}
