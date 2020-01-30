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
        private IMostWantedRepository _Repo;
        public HomeController()
        {
            _Repo = new DummyMostWantedRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var Model = _Repo.GetAll();
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