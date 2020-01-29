using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBI.Webpage.Services;
using FBI.Webpage.Models;

namespace FBI.Webpage.Controllers
{
    public class MostWantedController : Controller
    {
        private IMostWantedRepository _Repo;
        private IMostWantedRepository _RepoProfile;
        public string title;
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
        //Not SURE
        [HttpGet]
        public ActionResult MostWantedProfile()
        {
            return View(_RepoProfile.GetOne());
        }
        //Not SURE
        public MostWantedController(string title)
        {
            _RepoProfile = new DummyMostWantedRepository();

        }
        
    }
}