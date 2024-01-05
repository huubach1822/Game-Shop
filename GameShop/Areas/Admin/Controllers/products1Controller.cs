using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using GameShop.Models;
using PagedList;

namespace GameShop.Areas.Admin.Controllers
{
    public class products1Controller : Controller
    {
        private game_shop_webEntities db = new game_shop_webEntities();

        // GET: Admin/products1
        public ActionResult Index(string searchString,int? page)
        {
            List<product> lstProducst;
            if (!searchString.IsEmpty()){
               lstProducst = db.products.Where(n => n.name.Contains(searchString))
                    .Include(p => p.category).ToList();
            }
            else
            {
                lstProducst = db.products.Include(p => p.category).ToList();
            }
            int pagenumber = (page ?? 1);
            ViewBag.SearchString = searchString;
            return View(lstProducst.ToPagedList(pagenumber,10));
        }

        // GET: Admin/products1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/products1/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.categories, "id", "name");
            return View();
        }

        // POST: Admin/products1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(product product)
        {
                if (product.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                    string extenstion = Path.GetExtension(product.ImageUpload.FileName);
                    fileName = fileName + extenstion;
                    product.avatar_url = "~/Asset/admin/img/" + fileName;
                    product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Asset/admin/img/"), fileName));
                }
                if (product.multiImg != null)
                {
                    foreach (HttpPostedFileBase file in product.multiImg)
                    {
                        if (file != null)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            string extenstion = Path.GetExtension(file.FileName);
                            fileName = fileName + extenstion;
                            product_img_url productImg = new product_img_url();
                            productImg.img_url = "~/Asset/admin/img/" + fileName;
                            file.SaveAs(Path.Combine(Server.MapPath("~/Asset/admin/img/"), fileName));
                            product.product_img_url.Add(productImg);
                        }
                    }
                }
                product.updated_date = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


                ViewBag.category_id = new SelectList(db.categories, "id", "name", product.category_id);
                return View(product);
         
        }

        // GET: Admin/products1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
           
            ViewBag.category_id = new SelectList(db.categories, "id", "name", product.category_id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                    string extenstion = Path.GetExtension(product.ImageUpload.FileName);
                    fileName = fileName + extenstion;
                    product.avatar_url = "~/Asset/admin/img/" + fileName;
                    product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Asset/admin/img/"), fileName));
                }

                product.updated_date = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;

                if (product.multiImg != null)
                {

                    foreach (HttpPostedFileBase file in product.multiImg)
                    {
                        if (file != null)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            string extenstion = Path.GetExtension(file.FileName);
                            fileName = fileName + extenstion;
                            product_img_url productImg = new product_img_url();
                            productImg.img_url = "~/Asset/admin/img/" + fileName;
                            file.SaveAs(Path.Combine(Server.MapPath("~/Asset/admin/img/"), fileName));
                            product.product_img_url.Add(productImg);
                        }
                    }
                }
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.categories, "id", "name", product.category_id);
            return View(product);


        }

        // GET: Admin/products1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/products1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            product product = db.products.Find(id);
            List<product_img_url> listImg = db.product_img_url.Where(n => n.product_id == id).ToList();
            db.product_img_url.RemoveRange(listImg);
            db.products.Remove(product);
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
