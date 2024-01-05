using GameShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameShop.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        game_shop_webEntities db = new game_shop_webEntities();
        public ActionResult Index(int? id_cate, int? max, int? min,bool isstock = false ,bool ishot = false)
        {             
            ShopModel lst = new ShopModel();
            lst.categoryList = db.categories.ToList();
            List<product> listProduct = db.products.ToList();
            if (max != 0 && max >= min)
            {
                listProduct = listProduct.Where(n => n.price >= min && n.price <= max).ToList();
                
            }
            if (id_cate != 0 && id_cate != null)
            {
                listProduct = listProduct.Where(n => n.category_id.Equals(id_cate)).ToList();
                ViewBag.CategoryId = id_cate;               
            }           
            if (ishot == true)
            {
                listProduct = listProduct.Where(n => n.isHot == true).ToList();
            }
            if (isstock == true)
            {
                listProduct = listProduct.Where(n => n.quantity != 0).ToList();
            }
            lst.productList = listProduct;
            return View(lst);
        }
        
    }
}