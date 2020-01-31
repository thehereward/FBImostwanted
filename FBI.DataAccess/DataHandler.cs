using Npgsql;

namespace FBI.DataAccess
{

    public class DataHandler
    {
        string cs = "Host=localhost;Username=postgres;Password=Password0512!;Database=mostWanted";

        public void FillDB()
        {
            for (var i = 1; i <= 45; i++)
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
                            throw;
                            //TODO: logging
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
    }
}
