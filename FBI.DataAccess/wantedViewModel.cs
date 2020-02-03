using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBI.DataAccess
{
    public class Image
    {
        public string large { get; set; }
    }

    public class Item
    {

        public string uid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<Image> images { get; set; }
        public string warning_message { get; set; }
        public int reward_max { get; set; }
        public int reward_min { get; set; }
        public List<string> locations { get; set; }
        public string status { get; set; }
        public string nationality { get; set; }
        public List<string> possible_countries { get; set; }
        public List<string> possible_states { get; set; }

    }

    public class Root
    {
        public List<Item> items { get; set; }
    }
}
