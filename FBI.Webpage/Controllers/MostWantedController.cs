using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBI.Webpage.Services;

namespace FBI.Webpage.Controllers
{
    public class MostWantedController : Controller
    {
        // GET: MostWanted
        [HttpGet]
        public ActionResult Index()
        {
            return View(_Repo.GetAll());
        }
        public MostWantedController()
        {
            _Repo = new DummyMostWantedRepository();


        }
        private IMostWantedRepository _Repo;
    }
}