using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FBI.Webpage.Models;
using Newtonsoft.Json;
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}