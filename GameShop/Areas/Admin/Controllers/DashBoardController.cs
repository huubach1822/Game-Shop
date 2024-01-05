using GameShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;

namespace GameShop.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: Admin/DashBoard
        game_shop_webEntities db = new game_shop_webEntities();
        public ActionResult Index()
        {
            List<GameShop.Models.user> listUser = db.users.ToList(); 
            return View(db.users.ToList());

        }
        public PartialViewResult NavigationPartial()
        {
            return PartialView();
        }
        public PartialViewResult TopNav()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            user user = new user();

            return View(user);
        }
        [HttpPost]
        public ActionResult CreateUser(user user)
        {
            try
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
                ModelState.Clear();

                return RedirectToAction("Index", "DashBoard");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user =db.users.Find(id);
      
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(user user)
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
    }
}