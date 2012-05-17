using System;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Helpers;
using HavilaTravel.Models;

namespace HavilaTravel.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        //
        // GET: /Admin/Article/

        public ActionResult Add()
        {

            return View(new Article{Date = DateTime.Now.Date});
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var article = new Article();
                TryUpdateModel(article, new[] { "Title", "Date" });
                article.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.AddToArticle(article);
                context.SaveChanges();

                var mailText = article.Text.Replace("src=\"", "src=\"http://havila-travel.com/");
                var customers = context.Customers.Where(c=>c.IsActive==1).ToList();
                foreach (var customer in customers)
                {
                    mailText += "<br/><br/> Для того, чтобы отписаться от рассылке перейдите пожалуйста по следующей ссылке <br/>";
                    mailText += "<a href=\"http://havila-travel.com/unsubscribe/" + customer.Id + "\">http://havila-travel.com/unsubscribe/" + customer.Id + "</a>";
                    MailHelper.SendMessage(new MailAddress(customer.Email), mailText, "Рассылка Havila-Travel", true);
                }

            }
            return RedirectToAction("Index", "Articles", new { Area = "" });
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {
                var article = context.Article.First(a => a.Id == id);
                return View(article);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var article = context.Article.First(a => a.Id == id);
                TryUpdateModel(article, new[] { "Title", "Date" });
                article.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Articles", new { Area = "" });
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var article = context.Article.First(a => a.Id == id);
                context.DeleteObject(article);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Articles", new { Area = "" });
        }

    }
}
