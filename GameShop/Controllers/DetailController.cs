using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameShop.Models;


namespace GameShop.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        game_shop_webEntities db = new game_shop_webEntities();
        public ActionResult DetailProducts(int MaSP)
        {
            IndexClass banner = new IndexClass();
            banner.detailProduct = db.products.Where(n => n.id.Equals(MaSP)).FirstOrDefault();
            banner.lstImg = db.product_img_url.OrderByDescending(n => n.id).Where(n => n.product_id == MaSP).ToList();
            banner.productList = db.products.Take(4).OrderByDescending(n => n.create_at).ToList();
            return View(banner);

        }
    }
}