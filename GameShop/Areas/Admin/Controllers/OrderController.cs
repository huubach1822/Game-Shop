using GameShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Data.Entity.Migrations;

namespace GameShop.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private game_shop_webEntities db = new game_shop_webEntities();
        // GET: Admin/Order
        public ActionResult Index()
        {
            List<order> listOrd = db.orders.ToList();
            foreach(order item in listOrd)
            {
                int total = 0;
                List<order_product> listOP = db.order_product.Where(n=>n.orders_id.Equals(item.id)).ToList();
                foreach (order_product op in listOP)
                {
                    if(op.product.price_sale != 0)
                    {
                        total += op.product.price_sale;
                    }
                    else
                    {
                        total += op.product.price;
                    }
                }
                item.totalMoney = total;
            }
            

            return View(listOrd);
        }
        public ActionResult Approve(string email,int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<order_product> listP = db.order_product.Where(n=>n.orders_id.Equals(id)).ToList();
                    string activeCode="";
                    foreach (var item in listP)
                    {
                        activeCode += "ma active: " + item.product.active_code;
                    }
                    var senderEmail = new MailAddress("anh202612908@lms.utc.edu.vn", "Viet Anh Dz");
                    var receiverEmail = new MailAddress(email, "Receiver");
                    var password = "Bill2172002";
                    var sub = "Thong bao thanh toan";
                    var body = "Ban da thanh toan thanh cong" +
                        "Ma kich hoat cua ban la"+ activeCode;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };

                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                   order a=  db.orders.Where(n=>n.id.Equals(id)).SingleOrDefault();
                    a.status = 1;
                    db.orders.AddOrUpdate(a);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return RedirectToAction("index", ViewBag.Error);
        }
    }
}