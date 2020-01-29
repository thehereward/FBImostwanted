using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBI.Webpage.Models
{
    public class Image
    {
        public string caption { get; set; }
        public string original { get; set; }
        public string large { get; set; }
        public string thumb { get; set; }
    }

    public class File
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Item
    {

        public string uid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<Image> images { get; set; }
        public List<File> files { get; set; }
        public string warning_message { get; set; }
        public string remarks { get; set; }
        public string details { get; set; }
        public string additional_information { get; set; }
        public string caution { get; set; }
        public string reward_text { get; set; }
        public int reward_min { get; set; }
        public int reward_max { get; set; }
        public List<string> dates_of_birth_used { get; set; }
        public string place_of_birth { get; set; }
        public List<string> locations { get; set; }
        public List<string> field_offices { get; set; }
        public List<string> legat_names { get; set; }
        public string status { get; set; }
        public string person_classification { get; set; }
        public string ncic { get; set; }
        public int age_min { get; set; }
        public int age_max { get; set; }
        public int weight_min { get; set; }
        public int weight_max { get; set; }
        public int height_min { get; set; }
        public int height_max { get; set; }
        public string eyes { get; set; }
        public string hair { get; set; }
        public string build { get; set; }
        public string sex { get; set; }
        public string race { get; set; }
        public string nationality { get; set; }
        public string scars_and_marks { get; set; }
        public string complexion { get; set; }
        public string occupations { get; set; }
        public List<string> possible_countries { get; set; }
        public List<string> possible_states { get; set; }
        public string modified { get; set; }
        public string publication { get; set; }
        public string path { get; set; }
    }

    public class Root
    {
        public int total { get; set; }
        public int page { get; set; }
        public List<Item> items { get; set; }
    }
    

}
