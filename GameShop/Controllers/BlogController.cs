using GameShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameShop.Controllers
{
    public class BlogController : Controller
    {
        game_shop_webEntities db = new game_shop_webEntities();
        // GET: Blog
        public ActionResult Blog()
        {
            List<blog> lstBlog = db.blogs.ToList();
            return View(lstBlog);
        }
        public ActionResult DetailBlog(int Id)
        {
            BlogClass blog = new BlogClass();
            blog.DetailBlog = db.blogs.Where(n => n.id.Equals(Id)).FirstOrDefault();
            blog.blogList = db.blogs.Take(6).OrderByDescending(n => n.update_at).Where(n => n.id != Id).ToList();
            return View(blog);
        }
    }
}