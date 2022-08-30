using Microsoft.AspNet.Identity;
using SuperStore.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace SuperStore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Details(int ItemId)
        {
            var Item = db.Items.Find(ItemId);
            if (Item == null)
            {
                return HttpNotFound();
            }
            Session["ItemId"] = ItemId;
            return View(Item);
        }
        [Authorize]
        public ActionResult Buy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Buy(string Address)
        {
            var UserId = User.Identity.GetUserId();
            var ItemId = (int)Session["ItemId"];

            var Check = db.BuyItems.Where(a => a.ItemId == ItemId && a.UserId == UserId).ToList();

            if (Check.Count < 1)
            {
                var item = new BuyItem();

                item.UserId = UserId;
                item.ItemId = ItemId;
                item.Address = Address;
                item.BuyDate = DateTime.Now;

                db.BuyItems.Add(item);
                db.SaveChanges();
                ViewBag.Result = "Done";
            }
            else
            {
                ViewBag.Result = "You have already purchased";
            }



            return View();
        }

        [Authorize]
        public ActionResult GetItemsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Items = db.BuyItems.Where(a => a.UserId == UserId);
            return View(Items.ToList());
        }
        [Authorize]
        public ActionResult DetailsOfItem(int Id)
        {
            var Item = db.BuyItems.Find(Id);
            if (Item == null)
            {
                return HttpNotFound();
            }
            return View(Item);
        }
        [Authorize]
        public ActionResult GrtItemsByPublisher()
        {
            var UserID = User.Identity.GetUserId();

            var Items = from app in db.BuyItems
                        join item in db.Items
                        on app.ItemId equals item.Id
                        where item.User.Id == UserID
                        select app;
            var grouped = from i in Items
                          group i by i.item.ItemTitle
                          into gr
                          select new ItemsViewModel
                          {
                              Itemtitle = gr.Key,
                              Listes = gr
                          };

            return View(grouped.ToList());
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            var item = db.BuyItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(BuyItem item)
        {
            if (ModelState.IsValid)
            {
                item.BuyDate = DateTime.Now;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetItemsByUser");
            }
            return View(item);
        }

        public ActionResult Delete(int id)
        {
            var item = db.BuyItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(BuyItem item)
        {
            try
            {
                // TODO: Add delete logic here
                var myItem = db.BuyItems.Find(item.Id);
                db.BuyItems.Remove(myItem);
                db.SaveChanges();
                return RedirectToAction("GetItemsByUser");
            }
            catch
            {
                return View(item);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("Email", "Password");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("YourEmail"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "Name Client" + contact.Name + "<br>" + "Client Email" + contact.Email + "br" + "Adress Message" + contact.Subject + "<br>" + "Message" + contact.Message;
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com",587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Items.Where(a => a.ItemTitle.Contains(searchName)
            || a.ItemContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();
            return View(result);
        }

        public ActionResult Click(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
    }
}