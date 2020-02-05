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
        Item2 modelToPassAround = new Item2();
        List<string> imageListToPassAround = new List<string>();




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

        //[HttpPost]
        public ActionResult Edit(string uid)
        {

            //ViewBag.Message = "MOST WANTED Profile";
            //// Item singleProfile = root.items[index];
            //var testMostWantedProfile = new Item();
            //testMostWantedProfile.description = "THE TITLE of MOST wanted PERson";
            //testMostWantedProfile.description = "this is a DESCRIPTION of a most wanted person";
            //testMostWantedProfile.caution = "this is a CAUTION for this most wanted person";
            //var image1 = new Image();
            //var image2= new Image();

            //image1.large = "https://pncguam.com/wp-content/uploads/2013/07/michael%20tony.jpg";

            //image2.large = "https://kuam.images.worldnow.com/images/18564862_G.jpeg?auto=webp&disable=upscale&height=560&fit=bounds&lastEditedDate=1562204300000";
            //List<Image> ImageList = new List<Image>(new Image[] { image1, image2 });

            //testMostWantedProfile.images = ImageList;

            //testMostWantedProfile.nationality = "this is a NATIONALITY of a most wanted person";
            //testMostWantedProfile.images[1] = image2;
            //testMostWantedProfile.status = "na";
            //testMostWantedProfile.uid = "UID NUMBER";



          
            //imageListToPassAround = modelToPassAround.images.ToList();
            if (User.Identity.IsAuthenticated)
            {
                var model = new accountViewModel();
                model.fugitive = dataHandler.SelctOneRecordRandomly(uid);
                model.reports = dataHandler.reports(uid);
                return View(model);
            }
            else
            {

                return RedirectToAction("PostTheEditedProfile", "Home", new { uid });
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[HttpPost]
        public ActionResult PostTheEditedProfile(string uid)
        {
            var model = new accountViewModel();
            model.fugitive = dataHandler.SelctOneRecordRandomly(uid);
            model.reports = dataHandler.reports(uid);
            return View(model);
        }
        

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PostTheEditedProfile (Item2 form, HttpPostedFileBase file)
        {
            string imagePath2 = "";
            WebImage photo = null;
            var newFileName = "";
            var imagePath = "";
            Item2 Data = form;
           
            var dataFormat = new dataFormatHandler();
            if (ModelState.IsValid)
            {
                //try
                //{


                    if (file != null)
                    {

                        photo = WebImage.GetImageFromRequest();
                        if (photo != null)
                        {
                            newFileName = Guid.NewGuid().ToString() + "_" +
                                Path.GetFileName(photo.FileName);
                            imagePath = @"~/uploads/" + newFileName;

                            //string imagePath = @"images\" + photo.FileName;
                            //photo.Save(@"~\" + imagePath);
                            imagePath2= Server.MapPath(imagePath);
                        }
                            //Extract Image File Name.
                            //string fileName = System.IO.Path.GetFileName(file.FileName);

                            ////Set the Image File Path.
                            //string filePath = Path.Combine("~/UploadedFiles/", fileName); /*"~/UploadedFiles/" + fileName;*/

                            ////Save the Image File in Folder.
                        //file.SaveAs(Server.MapPath(imagePath2));
                        file.SaveAs(Server.MapPath(imagePath));
                        //Insert the Image File details in Table.
                        //FilesEntities entities = new FilesEntities();
                        //entities.Files.Add(new File
                        //{
                        //    Name = fileName,
                        //    Path = filePath
                        //});
                        //entities.SaveChanges();




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
                //}
                //catch (Exception)
                //{

                //    ViewBag.FileStatus = "Error while file uploading.";
                //}

            }
            
            Data.caution = dataFormat.stringIsNull(Data.caution);
            Data.description = dataFormat.stringIsNull(Data.description);
            Data.uid = dataFormat.stringIsNull(Data.uid);
            Data.title = dataFormat.stringIsNull(Data.title);
            //if (Data.reward_max = null)
            //{
            //    Data.reward_max = 0;
            //}
            if (Data.locations == null)
            {
                Data.locations = new string[] { "null" };
            }

            Data.status = dataFormat.stringIsNull(Data.status);
            Data.nationality = dataFormat.stringIsNull(Data.nationality);

           
            Data.locations[0] = dataFormat.stringIsNull(Data.locations[0]);



            dataHandler.UpdateAProfile(Data);
            return View(Data);
            
        }
    }
}