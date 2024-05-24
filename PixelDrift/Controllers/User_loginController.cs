using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PixelDrift.Models;

namespace PixelDrift.Controllers
{
    public class User_loginController : Controller
    {
        private pixeldrift_dbEntities1 db = new pixeldrift_dbEntities1();

        // GET: User_login
        public ActionResult Index()
        {
            return View(db.User_login.ToList());
        }

        // GET: User_login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_login user_login = db.User_login.Find(id);
            if (user_login == null)
            {
                return HttpNotFound();
            }
            return View(user_login);
        }

        // GET: User_login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_Id,Password,First_Name,Last_Name,Email_ID,Role,tab_Id")] User_login user_login)
        {
            if (ModelState.IsValid)
            {
                db.User_login.Add(user_login);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_login);
        }

        // GET: User_login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_login user_login = db.User_login.Find(id);
            if (user_login == null)
            {
                return HttpNotFound();
            }
            return View(user_login);
        }

        // POST: User_login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,Password,First_Name,Last_Name,Email_ID,Role,tab_Id")] User_login user_login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_login);
        }

        // GET: User_login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_login user_login = db.User_login.Find(id);
            if (user_login == null)
            {
                return HttpNotFound();
            }
            return View(user_login);
        }

        // POST: User_login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_login user_login = db.User_login.Find(id);
            db.User_login.Remove(user_login);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
