using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using GameShop.Models;
using PagedList;

namespace GameShop.Areas.Admin.Controllers
{
    public class Product_img_urlController : Controller
    {
        private game_shop_webEntities db = new game_shop_webEntities();

        // GET: Admin/Product_img_url
        public ActionResult Index(string searchString, int? page)
        {
            List<product_img_url> product_img_url;
            if (!searchString.IsEmpty())
            {
                    product_img_url = db.product_img_url
                    .Where(n => n.product.name.Contains(searchString))
                    .Include(p => p.product).ToList();
            }
            else
            {
                product_img_url = db.product_img_url.Include(p => p.product).ToList();
            }
            
            int pagenumber = (page ?? 1);
            ViewBag.SearchString = searchString;
            return View(product_img_url.ToPagedList(pagenumber,20));
        }

        // GET: Admin/Product_img_url/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_img_url product_img_url = db.product_img_url.Find(id);
            if (product_img_url == null)
            {
                return HttpNotFound();
            }
            return View(product_img_url);
        }

        // GET: Admin/Product_img_url/Create
        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.products, "id", "name");
            return View();
        }

        // POST: Admin/Product_img_url/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,img_url,product_id")] product_img_url product_img_url)
        {
            if (ModelState.IsValid)
            {
                db.product_img_url.Add(product_img_url);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.products, "id", "name", product_img_url.product_id);
            return View(product_img_url);
        }

        // GET: Admin/Product_img_url/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_img_url product_img_url = db.product_img_url.Find(id);
            if (product_img_url == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.products, "id", "name", product_img_url.product_id);
            return View(product_img_url);
        }

        // POST: Admin/Product_img_url/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,img_url,product_id")] product_img_url product_img_url)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_img_url).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.products, "id", "name", product_img_url.product_id);
            return View(product_img_url);
        }

        // GET: Admin/Product_img_url/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_img_url product_img_url = db.product_img_url.Find(id);
            if (product_img_url == null)
            {
                return HttpNotFound();
            }
            return View(product_img_url);
        }

        // POST: Admin/Product_img_url/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_img_url product_img_url = db.product_img_url.Find(id);
            db.product_img_url.Remove(product_img_url);
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
