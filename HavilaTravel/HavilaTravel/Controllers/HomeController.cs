using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using HavilaTravel.Helpers;
using HavilaTravel.Models;
using System.Web;

namespace HavilaTravel.Controllers
{
    public class HomeController : Controller
    {

        [HttpPost]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public PartialViewResult GetBellboy()
        {
            using (var context = new ContentStorage())
            {
                var bellboy = context.Bellboy.GetRandomItem();
                return PartialView("Bellboy", bellboy);
            }
        }

        public ActionResult NotFound()
        {
            using (var context = new ContentStorage())
            {
                SiteViewModel model = new SiteViewModel(null, context, true);
                return View(model);
            }
        }

        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {
                SiteViewModel model = new SiteViewModel(id, context, true);

                ViewBag.PageTitle = model.Content.PageTitle;
                ViewBag.SeoDescription = model.Content.SeoDescription;
                ViewBag.SeoKeywords = model.Content.SeoKeywords;

                return View(model);
            }
        }

        public ActionResult Search(string query)
        {
            using (var context = new ContentStorage())
            {
                SiteViewModel model = new SiteViewModel("countries", context, false)
                                          {
                                              SearchResult = new List<Content>(), 
                                              SearchQuery = query
                                          };

                ViewBag.PageTitle = "Результат поиска";

                if (string.IsNullOrEmpty(query))
                {
                    return View(model);
                }

                query = query.ToLower();

                var contentItems = context.Content.Include("Parent").Include("Accordions").Where(c => c.PlaceKind > 1).ToList();
                foreach (var content in contentItems)
                {
                    content.Text = HttpUtility.HtmlDecode(content.Text);
                    content.Text = Regex.Replace(content.Text, @"<[^>]+>", String.Empty).Replace("\r", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty);
                    int strLength = content.Text.Length;
                    if (strLength > 500) strLength = 500;
                    content.Text = content.Text.Substring(0, strLength);
                    foreach (var accordion in content.Accordions)
                    {
                        accordion.AccordionImages.Load();
                    }

                    if (content.Title.ToLower().Contains(query) || content.MenuTitle.ToLower().Contains(query) || content.Text.ToLower().Contains(query))
                        model.SearchResult.Add(content);

                }
                return View(model);
            }
        }
    }
}
