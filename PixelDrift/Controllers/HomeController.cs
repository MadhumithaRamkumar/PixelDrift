﻿using System;
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

            if (user > 0)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {

                System.Windows.Forms.MessageBox.Show("Incorrect User Name and Password");
                return View();
            }
        }

        //public ActionResult CreateUser()
        //{
        //    return View();
        //}

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

                      
                }
                    
            }
            return View(u);


        


        }

        public ActionResult Dashboard()
        {
            return View();

        }

        public ActionResult Upload()
        {
            CloudBlobContainer blobConatiner = _blobStorageService.GetCloudBlobContainer();
            List<string> blobs = new List<string>();
            foreach( var blobItem in blobConatiner.ListBlobs())
            { blobs.Add(blobItem.Uri.ToString());
            }
            return View(blobs);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase image)
        {
            if(image!=null)
                if(image.ContentLength>0)
                {
                    CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(image.FileName);
                    blob.UploadFromStream(image.InputStream);
                    
                }
            return RedirectToAction("Upload");
        }
    }
}