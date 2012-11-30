using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Helpers;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        

        public ActionResult Edit(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var content = context.Content.First(c => c.Id == id);
                    TryUpdateModel(content, new[] {"Title","DescriptionTitle","SeoDescription","SeoKeywords","SortOrder"});
                    content.Text = HttpUtility.HtmlDecode(form["Text"]);
                    content.Description = HttpUtility.HtmlDecode(form["Description"]);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home", new {area="", id = content.Name});
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Index()
        {
            using (var context = new CatalogueContainer())
            {
                var images = context.MainPageImage.ToList();
                return View(images);
            }
        }

        public ActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddImage(IEnumerable<HttpPostedFileBase> fileUpload)
        {
            using (var context = new CatalogueContainer())
            {
                foreach (var file in fileUpload)
                {
                    if (file != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        file.SaveAs(filePath);
                        var mpi = new MainPageImage() {ImageSource = fileName};
                        context.AddToMainPageImage(mpi);
                        context.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Home", new {area = ""});
            }
        }

        public ActionResult DeleteImage(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var image = context.MainPageImage.First(i => i.Id == id);
                ImageHelper.DeleteImage(image.ImageSource);
                context.DeleteObject(image);
                context.SaveChanges();
                return RedirectToAction("Index", "Content", new { area = "Admin",id="" });
            }
        }
    }
}
