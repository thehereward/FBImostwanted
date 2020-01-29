using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FBI.Webpage.Models;
using Newtonsoft.Json;
using FBI.DataAccess;



namespace FBI.Webpage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var json = new WebClient().DownloadString("https://api.fbi.gov/@wanted");
            var roots = JsonConvert.DeserializeObject<Root>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return View(roots);
            
        }


        public ActionResult Edit(Root root, int index)
        {
            ViewBag.Message = "MOST WANTED Profile";
            Item signleProfile = root.items[index];
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}