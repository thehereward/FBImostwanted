using Npgsql;
using System.Collections.Generic;
using FBI.DataAccess;
using Newtonsoft.Json;
using System.Net;



namespace FBI.DataAccess
{
    public interface IMostWantedRepository
    {
        List<Item> GetAll();
        
        void GetfromDb ();
    }
    
    public class DummyMostWantedRepository : IMostWantedRepository
    {
        string cs = "Host=localhost;Username=postgres;Password=password;Database=FBImostwanted";

        public List<Item> GetAll()
        {
            return new List<Item>();
        }
        
        
        //var books = connection.Query<Book>("SELECT * FROM books ORDER BY title ASC").ToList();
        //    return books;
        public void GetfromDb()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();

                var str = "SELECT uid, title, images" +
                            "FROM item " +
                            "WHERE title = @title";
                List<Item> fugitivesList = new List<Item>();
            }
        }

    }
}
