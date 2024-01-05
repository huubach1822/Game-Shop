using GameShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        game_shop_webEntities db = new game_shop_webEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register ()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register (user user)
        {
         
                user.permission = 0;        
                user.create_at = DateTime.Now;
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "DashBoard");
      /*      }

            return RedirectToAction("Register");*/
        }
        [HttpPost]
        public ActionResult Autherize(GameShop.Models.user userModel)
        {
            var userDetail = db.users.Where(x => x.name==userModel.name
            && x.password ==userModel.password)
                .FirstOrDefault();
            if (userDetail == null)
            {
                userModel.LoginErrorMessage = "Wrong username or password";
                return View("Index",userModel);
                
            }
            if(userDetail.permission == 0)
            {
                // lay thong tin khach hang tai day
                Session["norUser"] = userDetail;
                return RedirectToAction("Index", "Index", new {area = ""});
            }
            if(userDetail.permission != 1)
            {
                userModel.LoginErrorMessage = "You dont have permission";
                return View("Index", userModel);
            }
            else
            {
                Session["userName"] = userDetail.name;
                Session["userId"] = userDetail.id;
                Session["avatarUrl"] = userDetail.img_url;
                return RedirectToAction("Index", "DashBoard");
            }
        }
        public ActionResult LogOut()
        {
            Session["userName"] = null;
            Session["userId"] =  null;
            Session["avatarUrl"] = null;
            return RedirectToAction("Index");
        }
       
    }
}