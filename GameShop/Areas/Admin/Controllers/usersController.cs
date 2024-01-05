using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameShop.Models;

namespace GameShop.Areas.Admin.Controllers
{
    public class usersController : Controller
    {
        private game_shop_webEntities db = new game_shop_webEntities();

        // GET: Admin/users
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        // GET: Admin/users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(user user)
        {
            if (ModelState.IsValid)
            {
                if (user.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(user.ImageUpload.FileName);
                    string extenstion = Path.GetExtension(user.ImageUpload.FileName);
                    fileName = fileName + extenstion;
                    user.img_url = "~/Asset/admin/img/" + fileName;
                    user.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Asset/admin/img/"), fileName));
                }

                user.create_at = DateTime.Now;
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( user user)
        {
            if (ModelState.IsValid)
            {
                if (user.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(user.ImageUpload.FileName);
                    string extenstion = Path.GetExtension(user.ImageUpload.FileName);
                    fileName = fileName + extenstion;
                    user.img_url = "~/Asset/admin/img/" + fileName;
                    user.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Asset/admin/img/"), fileName));
                }

                user.create_at = DateTime.Now;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
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
