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


        public ActionResult Edit()
        {
            
            ViewBag.Message = "MOST WANTED Profile";
            // Item singleProfile = root.items[index];
            var testMostWantedProfile = new Item();
            testMostWantedProfile.description = "THE TITLE of MOST wanted PERson";
            testMostWantedProfile.description = "this is a DESCRIPTION of a most wanted person";
            testMostWantedProfile.caution = "this is a CAUTION for this most wanted person";
            var image1 = new Image();
            var image2= new Image();
           
            image1.thumb = "https://pncguam.com/wp-content/uploads/2013/07/michael%20tony.jpg";
            
            image2.thumb = "https://kuam.images.worldnow.com/images/18564862_G.jpeg?auto=webp&disable=upscale&height=560&fit=bounds&lastEditedDate=1562204300000";
            List<Image> ImageList = new List<Image>(new Image[] { image1, image2 });

            testMostWantedProfile.images = ImageList;

            testMostWantedProfile.nationality = "this is a NATIONALITY of a most wanted person";
            testMostWantedProfile.images[1] = image2;
            testMostWantedProfile.status = "na";
            testMostWantedProfile.uid = "UID NUMBER";
            return View(testMostWantedProfile);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public void PostTheEditedProfile (Models.Item formData)
        {

        }
    }
}