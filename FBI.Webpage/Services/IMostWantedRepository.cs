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
    }
    public class DummyMostWantedRepository: IMostWantedRepository
    {
        public List<Item> GetAll()
        {
            return new List<Item>();
        }

        
    }
}
