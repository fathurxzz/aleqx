using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Helpers;
using HavilaTravel.Models;

namespace HavilaTravel.Areas.Admin.Controllers
{
    [Authorize]
    public class AccordionController : Controller
    {
        //
        // GET: /Admin/Accordion/
        public ActionResult Create(int id)
        {
            ViewBag.parentId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(int id, int parentId, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();

                var accordion = new Accordion();
                TryUpdateModel(accordion, new[]
                                              {
                                                  "Title",
                                                  "SortOrder"
                                              });

                accordion.Text = HttpUtility.HtmlDecode(form["Text"]);
                accordion.Content = content;
                context.AddToAccordion(accordion);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {
                var accordion = context.Accordion.Where(a => a.Id == id).First();
                return View(accordion);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var accordion = context.Accordion.Include("Content").Where(a => a.Id == id).First();
                var content = accordion.Content;

                TryUpdateModel(accordion, new[] {"Title", "SortOrder"});
                accordion.Text = HttpUtility.HtmlDecode(form["Text"]);

                context.SaveChanges();

                return RedirectToAction("Index", "Home", new {id = content.Name, area = ""});
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var accordion = context.Accordion.Include("Content").Include("AccordionImages").Where(a => a.Id == id).First();
                var content = accordion.Content;

                while (accordion.AccordionImages.Any())
                {
                    var image = accordion.AccordionImages.First();
                    IOHelper.DeleteFile("~/Content/Photos", image.ImageSource);
                    context.DeleteObject(image);
                }
                
                context.DeleteObject(accordion);
                
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }

        }

        public ActionResult AddPhoto(int id)
        {
            ViewBag.parentId = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(int parentId, IEnumerable<HttpPostedFileBase> fileUpload, IList<string> fileTitles )
        {
            using (var context = new ContentStorage())
            {
                var accordion = context.Accordion.Include("Content").Where(a => a.Id == parentId).First();


                int titleIndex = 0;
                foreach (var file in fileUpload)
                {
                    
                    if (file == null) continue;

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Photos", file.FileName);
                    string filePath = Server.MapPath("~/Content/Photos");
                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);

                    context.AddToAccordionImage(new AccordionImage
                                                    {
                                                        ImageSource = fileName,
                                                        Accordion = accordion,
                                                        Title = fileTitles[titleIndex]
                                                    });
                    context.SaveChanges();
                    titleIndex++;
                }
                


                return RedirectToAction("Index", "Home", new {id = accordion.Content.Name, area = ""});
            }
        }

        public ActionResult DeletePhoto(int id)
        {
            using (var context = new ContentStorage())
            {
                var image = context.AccordionImage.Include("Accordion").Where(i => i.Id == id).First();
                var accordion = image.Accordion;

                IOHelper.DeleteFile("~/Content/Photos", image.ImageSource);
                context.DeleteObject(image);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = accordion.Content.Name, area = "" });
            }
        }

    }
}
