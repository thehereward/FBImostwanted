using System;
using System.Collections.Generic;
using System.Text;

namespace FBI.DataAccess
{
    public class MostWantedProfilesModel
    {
        public class Image
        {
            public string large { get; set; }
        }

        public class Item2
        {

            public string uid { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string[] images { get; set; }
            public string caution { get; set; }
            public int reward_max { get; set; }
            public int reward_min { get; set; }
            public string[] locations { get; set; }
            public string status { get; set; }
            public string nationality { get; set; }
            public string[] possible_countries { get; set; }
            public string[] possible_states { get; set; }
            public string file { get; set; }

        }

        public class Root2
        {
            public List<Item2> items { get; set; }
        }
    }
}
