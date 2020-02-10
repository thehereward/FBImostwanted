using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FBI.Webpage.Models;
using Newtonsoft.Json;
using FBI.DataAccess;
using Npgsql;
using static FBI.DataAccess.MostWantedProfilesModel;
using System.IO;
using System.Web.Helpers;

namespace FBI.Webpage.Controllers
{
    //string cs = "Provider=PostgreSQL OLE DB Provider;Data Source=myServerAddress;location=ISpy;User ID=ISpy;password=pass123;timeout=1000;";
    public class HomeController : Controller
    {


        DataHandler dataHandler = new DataHandler();





        [HttpGet]
        public ActionResult Index()
        {
            var Model = dataHandler.GetFromDB();
            return View(Model);
        }

        [Authorize]
        public ActionResult ApproveReport(int report, string uid)
        {
            var datahandler = new DataHandler();
            datahandler.approveSighting(report);
            return RedirectToAction("Edit", "Home", new { uid = uid });
        }

        public ActionResult AddReport(ReportModel report, string uid)
        {
            var datahandler = new DataHandler();
            datahandler.ReportSighting(report);
            return RedirectToAction("PostTheEditedProfile", "Home", new { uid = uid });
        }

        public ActionResult DeleteReport(int sid, string uid)
        {
            var dataHandler = new DataHandler();
            dataHandler.DeleteSighting(sid);
            return RedirectToAction("Edit", "Home", new { uid = uid });
        }

       


        //Shows edit screen when edit button is pressed in profile view
        public ActionResult Edit(string uid)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new accountViewModel();
                model.fugitive = dataHandler.SelctOneRecord(uid);
                model.reports = dataHandler.reports(uid);
                return View(model);
            }
            else
            {

                return RedirectToAction("ViewProfile", "Home", new { uid });
            }
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        //shows profile from home screen
        public ActionResult ViewProfile(string uid)
        {
            var model = new accountViewModel();
            model.fugitive = dataHandler.SelctOneRecord(uid);
            model.reports = dataHandler.reports(uid);
            return View(model);
        }
        
        //shows profile after clicking submit button on Edit a Profile Screen
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ViewProfile(Item2 form, HttpPostedFileBase file)
        {
            string imagePath2 = "";
            WebImage photo = null;
            var newFileName = "";
            var imagePath = "";
            Item2 Data = form;
           
            var dataFormat = new dataFormatHandler();
            if (ModelState.IsValid)
            {



                //---***vvv FOR FUTURE IMAGE PROCESSING FROM EDIT SCREEN vvv***---{
                if (file != null)
                {
                    photo = WebImage.GetImageFromRequest();
                    if (photo != null)
                    {
                        newFileName = Guid.NewGuid().ToString() + "_" +
                            Path.GetFileName(photo.FileName);
                        imagePath = @"~/uploads/" + newFileName;
                        imagePath2 = Server.MapPath(imagePath);
                    }

                    file.SaveAs(Server.MapPath(imagePath));
                    List<string> tempImages = Data.images.ToList();
                    tempImages.Add(imagePath);
                    tempImages.Add(imagePath2);
                    Data.images = tempImages.ToArray();
                }
                else
                {
                    Data.file = dataFormat.stringIsNull(Data.file);
                    Data.images[0] = dataFormat.stringIsNull(Data.images[0]);
                }
                ViewBag.FileStatus = "File uploaded successfully.";
                //}---***^^^ FOR FUTURE IMAGE PROCESSING FROM EDIT SCREEN ^^***---

            }

            //Formats Data to prevent nulls before sending to data handler (probably could be moved to data handler in future):
            Data.caution = dataFormat.stringIsNull(Data.caution);
            Data.description = dataFormat.stringIsNull(Data.description);
            Data.uid = dataFormat.stringIsNull(Data.uid);
            Data.title = dataFormat.stringIsNull(Data.title);

            if (Data.locations == null)
            {
                Data.locations = new string[] { "null" };
            }
            if (Data.locations[0] == null)
            {
                Data.locations[0] = dataFormat.stringIsNull(Data.locations[0]);
            }
            Data.status = dataFormat.stringIsNull(Data.status);
            Data.nationality = dataFormat.stringIsNull(Data.nationality);


            //Send the data to datahandler for updating the profile
            dataHandler.UpdateAProfile(Data);
            return View(Data);
            
        }
    }
}