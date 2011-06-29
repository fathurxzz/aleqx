using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Babich.Models;

namespace Babich.Areas.Admin.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Admin/Gallery/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int id)
        {
            ViewData["id"] = id;
            using (var context = new ContentStorage())
            {
                var content = context.Content.Include("Galleries").Where(c => c.Id == id).First();
                int sortOrder = content.Galleries.Count>0? content.Galleries.Max(c => c.SortOrder):-1;
                ViewData["SortOrder"] = (sortOrder + 1).ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Add(int id, FormCollection form)
        {
            ViewData["id"] = id;

            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();

                var gallery = new Gallery
                                      {
                                          Content = content,
                                          Description = form["description"],
                                          SortOrder = Convert.ToInt32(form["SortOrder"])
                                      };



                context.AddToGallery(gallery);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }

        }



    }
}
