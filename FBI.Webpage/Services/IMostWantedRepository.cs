using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBI.Webpage.Models;



namespace FBI.Webpage.Services
{
    public interface IMostWantedRepository
    {
        List<Item> GetAll();

        //Not SURE
        List<Item> GetOne();
    }
    
    public class DummyMostWantedRepository: IMostWantedRepository
    {
        public Item title;

        public List<Item> GetAll()
        {
            return new List<Item>();
        }

        //Not SURE
        public List<Item> GetOne()
        {
            return new List<Item>();
        }

    }
}
