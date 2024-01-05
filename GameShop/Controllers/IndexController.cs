using GameShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace GameShop.Controllers
{
    public class IndexController : Controller
    {
        game_shop_webEntities db = new game_shop_webEntities();
        // GET: Index
        public ActionResult Index(string SearchString)
        {
            IndexClass banner = new IndexClass();
            if (SearchString == null)
            {
                ViewBag.TitleIndex = "New product";
                banner.productList = db.products.OrderByDescending(n => n.create_at).Take(8).ToList();
            } else
            {
                ViewBag.TitleIndex = "Search results";
                banner.productList = db.products.Where(n => n.name.Contains(SearchString)).Take(8).ToList();
                if (banner.productList.Count == 0)
                {
                    ViewBag.TitleIndex = "No games found";
                }
            }
            
            banner.banner = db.banners.Take(1).OrderByDescending(n => n.create_at).SingleOrDefault();
            banner.bannerList = db.banners.Take(4).OrderByDescending(n => n.create_at).Skip(1).ToList();
            

            banner.categories = new HashSet<string>();
            foreach (var item in banner.productList)
            {
                banner.categories.Add(item.category.name);
            }

            if (Session["listCart"] != null)
            {
                List<product> list = (List<product>)Session["listCart"];
                banner.numberOfProcduct = list.Count();
            }


            banner.hotProduct = db.products.Where(n => n.isHot == true).Take(3).ToList();
            banner.featureProduct = db.products.OrderByDescending(n => n.rate).Take(3).ToList();
            banner.bestsellerProduct = new List<product>();


            var q = (from a in db.order_product
                     group a by new { a.product_id } into b
                     select new
                     {
                         b.Key.product_id,
                         sumQuantity = b.Sum(s => s.quantity)
                     }).OrderByDescending(i => i.sumQuantity).Take(3).ToList();
            
            foreach( var item in q)
            {
                banner.bestsellerProduct.Add(db.products.Where(n => n.id == item.product_id).FirstOrDefault());
            };

            

            return View(banner);
        }
        public PartialViewResult cart()
        {
            ViewBag.number = 0;
            if (Session["listCart"] != null)
            {

                List<product> list = (List<product>)Session["listCart"];
                ViewBag.number = list.Count();
            }
            return PartialView();
        }
    }
}