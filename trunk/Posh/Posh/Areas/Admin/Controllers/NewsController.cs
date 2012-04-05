using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Posh.Helpers;
using Posh.Models;

namespace Posh.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult Create()
        {
            return View(new News { Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var newsItem = new News();
                TryUpdateModel(newsItem, new[] { "Title", "Date" });
                newsItem.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.AddToNews(newsItem);

                // TODO: change absolute url!!!!!
                //var mailText = HttpUtility.HtmlDecode(form["Text"]).Replace("src=\"", "src=\"http://havila-travel.com/");
                //var mailSubject = "Posh. Рассылка новостей.";
                //var subscribers = context.Subscriber.ToList();
                //foreach (var subscriber in subscribers)
                //{
                //    new MailHelper("mail@mail.com", "POSH").SendMessage(new MailAddress(subscriber.Email), mailText, mailSubject, true);
                //}

                context.SaveChanges();
            }
            return RedirectToAction("Index", "News", new { Area = "" });
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ModelContainer())
            {
                var newsItem = context.News.First(n => n.Id == id);
                return View(newsItem);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var newsItem = context.News.First(n => n.Id == id);
                TryUpdateModel(newsItem, new[] { "Title", "Date" });
                newsItem.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "News", new { Area = "" });
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ModelContainer())
            {
                var newsItem = context.News.First(n => n.Id == id);
                context.DeleteObject(newsItem);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "News", new { Area = "" });
        }
    }
}
