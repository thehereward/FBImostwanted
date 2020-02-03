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
        
        DataHandler dataHandler = new DataHandler();
        //public HomeController()
        //{
        //    DataHandler dataHandler = new DataHandler();
        //}

        [HttpGet]
        public ActionResult Index()
        {
            var Model = dataHandler.GetFromDB();
            return View(Model);
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