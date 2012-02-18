using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class ArticlesController : Controller
    {
        //
        // GET: /Articles/

        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var model = new SiteViewModel("articles", context, false);
                ViewBag.PageTitle = model.Content.PageTitle;
                ViewBag.SeoDescription = model.Content.SeoDescription;
                ViewBag.SeoKeywords = model.Content.SeoKeywords;
                var articles = context.Article.OrderBy(a => a.Date).ToList();
                foreach (var article in articles)
                {
                    article.Text = Regex.Replace(article.Text, @"<[^>]+>", String.Empty).Replace("\r", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty);
                    int strLength = article.Text.Length;
                    if (strLength > 500) strLength = 500;
                    article.Text = article.Text.Substring(0, strLength);
                }

                //{
                model.Articles = articles;
                
                //};
                return View(model);
            }
        }

        public ActionResult Details(int id)
        {
            using (var context = new ContentStorage())
            {
                var model = new SiteViewModel("articles", context, false)
                {
                    Article = context.Article.First(a => a.Id == id)
                };
                ViewBag.PageTitle = model.Article.Title;
                return View(model);
            }
        }

    }
}
