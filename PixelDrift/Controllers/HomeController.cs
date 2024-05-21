using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Microsoft.WindowsAzure.Storage.Blob;
using PixelDrift.Data;
using PixelDrift.Models;


namespace PixelDrift.Controllers
{
    public class HomeController : Controller
    {

        private AppDbContext _dbContext;
        AzureBlobService _blobStorageService = new AzureBlobService();
        public User_login current_User = new User_login();
        public string sessionuser;



        public HomeController()
        {


            _dbContext = new AppDbContext();
           
        }



        public ActionResult Index()
        {
            return View(new User_login());
        }

        public ActionResult DashboardTest()
        {
            return View();

        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Index(User_login u)
        {
             
              var user= _dbContext.user_Logins.Where(x=>x.User_Id == u.User_Id).Count();
            sessionuser = u.User_Id;

            if (user > 0)
            {
                current_User = _dbContext.user_Logins.Where(x => x.User_Id == u.User_Id).First();
                Session["User"] = u.User_Id;
                if (current_User.Role == "Admin")
                {
                    TempData["UserId"] = current_User.User_Id;
                    TempData["Role"] = "Admin";
                    return RedirectToAction("Dashboard");

                }
                else
                {
                    TempData["UserId"] = current_User.User_Id;
                    TempData["Role"] = "User";
                    return RedirectToAction("DashboardUser");
                }  // return RedirectToAction("DashboardUser");
            }
            else
            {

                System.Windows.Forms.MessageBox.Show("Incorrect User Name/Password");
                return View();
            }
        }

       

        [HttpPost]
        public ActionResult CreateUser(User_login u)
        {
            if (ModelState.Count != 0)
            {
                var user = _dbContext.user_Logins.Where(x => x.User_Id == u.User_Id).Count();
                if (user != 0)
                {
                    System.Windows.Forms.MessageBox.Show("USER EXIST");
                    return RedirectToAction("Dashboard");
                }
                else if (user == 0)
                {
                    _dbContext.user_Logins.Add(u);
                    _dbContext.SaveChanges();

                    System.Windows.Forms.MessageBox.Show("User Added Successfully");
                    return RedirectToAction("Dashboard");

                }
                    
            }
            return View(u);
        }

        public ActionResult CreateUserFreshPage()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();

        }

        public ActionResult DashboardUser(string user)
        {
            //var message = TempData["UserId"] as string;
            //ViewData["UserId"]= message;
            return View();

        }

        public ActionResult Upload(string u)
        {
           
             string user_blob= (string)Session["User"]; ;
            CloudBlobContainer blobConatiner = _blobStorageService.GetCloudBlobContainer();
            List<string> fileName = new List<string>();

            fileName = _dbContext.ImageSave.Where(x => x.User_Id == user_blob).Select(p => p.FileName).ToList();


            List<string> blobs = new List<string>();
            foreach( var blobItem in blobConatiner.ListBlobs())
            { 
                foreach(var name in fileName)
                    if(blobItem.Uri.ToString().Contains(name))
                blobs.Add(blobItem.Uri.ToString());
            }
            return View(blobs);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase image)
        {
           


            if (image!=null)
                if(image.ContentLength>0)
                {
                    CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(image.FileName);
                    blob.UploadFromStream(image.InputStream);
                    ImageSave imagedetails = new ImageSave();
                    imagedetails.User_Id = (string)Session["User"];
                    imagedetails.FileName = image.FileName;
                    _dbContext.ImageSave.Add(imagedetails);
                    _dbContext.SaveChanges();



                }
            return RedirectToAction("Upload");
        }


        public ActionResult Delete()
        {
            string user_blob = (string)Session["User"]; 
            CloudBlobContainer blobConatiner = _blobStorageService.GetCloudBlobContainer();
            List<string> fileName = new List<string>();

            fileName = _dbContext.ImageSave.Where(x => x.User_Id == user_blob).Select(p => p.FileName).ToList();


            List<string> blobs = new List<string>();
            foreach (var blobItem in blobConatiner.ListBlobs())
            {
                foreach (var name in fileName)
                    if (blobItem.Uri.ToString().Contains(name))
                        blobs.Add(blobItem.Uri.ToString());
            }
            return View(blobs);
        }

        [HttpPost]
        public ActionResult Delete(HttpPostedFileBase image)
        {



            if (image != null)
                if (image.ContentLength > 0)
                {
                    CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(image.FileName);
                    blob.UploadFromStream(image.InputStream);
                    ImageSave imagedetails = new ImageSave();
                    imagedetails.User_Id = (string)Session["User"];
                    imagedetails.FileName = image.FileName;
                    _dbContext.ImageSave.Remove(imagedetails);
                    _dbContext.SaveChanges();



                }
            return RedirectToAction("Delete");
        }

        //View Images code

        public ActionResult ViewImage()
        {
            string user_blob = (string)Session["User"];
            CloudBlobContainer blobConatiner = _blobStorageService.GetCloudBlobContainer();
            List<string> fileName = new List<string>();

            fileName = _dbContext.ImageSave.Where(x => x.User_Id == user_blob).Select(p => p.FileName).ToList();


            List<string> blobs = new List<string>();
            foreach (var blobItem in blobConatiner.ListBlobs())
            {
                foreach (var name in fileName)
                    if (blobItem.Uri.ToString().Contains(name))
                        blobs.Add(blobItem.Uri.ToString());
            }
            return View(blobs);
        }

        [HttpPost]
        public ActionResult ViewImage(HttpPostedFileBase image)
        {



            if (image != null)
                if (image.ContentLength > 0)
                {
                    CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(image.FileName);
                    blob.UploadFromStream(image.InputStream);
                    ImageSave imagedetails = new ImageSave();
                    imagedetails.User_Id = (string)Session["User"];
                 



                }
            return RedirectToAction("ViewImage");
        }

        [HttpPost]

        public string   DeleteImage(String Name)
        {
            Uri uri = new Uri(Name);
            string fileName = System.IO.Path.GetFileName(uri.LocalPath);

            CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileName);

            blob.Delete();
            return "file Deleted";
        }

        [HttpPost]

        public string ShareImage(String Name)
        {
            Uri uri = new Uri(Name);
            string fileName = System.IO.Path.GetFileName(uri.LocalPath);
            string user_share = (string)Session["ShareUser"];
            ImageSave Im_Save = new ImageSave();
            CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileName);
            Im_Save.FileName = fileName;
            Im_Save.User_Id = user_share;
            _dbContext.ImageSave.Add(Im_Save);
            _dbContext.SaveChanges();

          
            return "File Shared";
        }

        public ActionResult ShareName()
        {
            return View();
        }
        public ActionResult Share()
        {




            string user_blob = (string)Session["User"];
            CloudBlobContainer blobConatiner = _blobStorageService.GetCloudBlobContainer();
            List<string> fileName = new List<string>();

            fileName = _dbContext.ImageSave.Where(x => x.User_Id == user_blob).Select(p => p.FileName).ToList();


            List<string> blobs = new List<string>();
            foreach (var blobItem in blobConatiner.ListBlobs())
            {
                foreach (var name in fileName)
                    if (blobItem.Uri.ToString().Contains(name))
                        blobs.Add(blobItem.Uri.ToString());
            }
            return View(blobs);
        }

        [HttpPost]
        public ActionResult Share(HttpPostedFileBase image, User_login u)
        {
            if(u!=null)
            {
                Session["ShareUser"] = u.User_Id;
            }


            if (image != null)
                if (image.ContentLength > 0)
                {
                    CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(image.FileName);
                    blob.UploadFromStream(image.InputStream);
                    ImageSave imagedetails = new ImageSave();
                    imagedetails.User_Id = (string)Session["User"];
                    imagedetails.FileName = image.FileName;
                    _dbContext.ImageSave.Add(imagedetails);
                    _dbContext.SaveChanges();



                }
            return RedirectToAction("Share");
        }
     
    }
}