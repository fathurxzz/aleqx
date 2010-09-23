using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Models;

namespace Excursions.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        #region Excursions

        //public ActionResult Excursions()
        //{
        //    using (var context = new ContentStorage())
        //    {
        //        var excursionList = context.Excursion.Select(e => e).OrderBy(e => e.SortOrder).ToList();
        //        return View(excursionList);
        //    }
        //}

        public ActionResult AddExcursion()
        {
            ViewData["Id"] = int.MinValue;
            return View();
        }

        public ActionResult EditExcursion(int id)
        {
            using (var context = new ContentStorage())
            {
                Excursion excursion = context.Excursion.Select(e => e).Where(e => e.Id == id).First();
                return View(excursion);
            }
        }

        public ActionResult DeleteExcursion(int id)
        {

            return RedirectToAction("Index", "Excursions");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateExcursion(int id, string name, string title, string text, string description, string keywords, int sortOrder)
        {
            using (var context = new ContentStorage())
            {
                Excursion excursion = id != int.MinValue ? context.Excursion.Select(c => c).Where(c => c.Id == id).First():new Excursion();
                excursion.Date = DateTime.Now;
                excursion.Name = name;
                excursion.Text = text;
                excursion.SortOrder = sortOrder;
                excursion.Title = title;
                if(excursion.Id==0)
                    context.AddToExcursion(excursion);
                context.SaveChanges();
                return RedirectToAction("Index", "Excursions");
            }
        }


        #endregion

        #region Content

        //public ActionResult Content()
        //{
        //    return View();
        //}

        public ActionResult AddContentItem()
        {
            return View();
        }

        public ActionResult EditContentItem(int id)
        {

            using (var context = new ContentStorage())
            {
                var contentItem = context.Content.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
                return View(contentItem);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateContent(int id, string contentId, string title, string description, string keywords, string text, int sortOrder)
        {
            using (var context = new ContentStorage())
            {
                Content content = id != int.MinValue ? context.Content.Select(c => c).Where(c => c.Id == id).First() : new Content();
                content.ContentId = contentId;
                content.Title = title;
                content.Description = description;
                content.Keywords = keywords;
                content.Text = HttpUtility.HtmlDecode(text);
                content.SortOrder = sortOrder;

                if (content.Id == 0)
                    context.AddToContent(content);
                context.SaveChanges();
                return RedirectToAction("Index", "Content", new { id = contentId });
            }
        }

        public ActionResult DeleteContentItem(int id)
        {
            using (var context = new ContentStorage())
            {
                Content content = context.Content.Include("Children").Where(c => c.Id == id).FirstOrDefault();
                context.DeleteObject(content);
                context.SaveChanges();
                return RedirectToAction("Index", "Content", new { id = "About" });
            }

        }

        #endregion


    }
}
