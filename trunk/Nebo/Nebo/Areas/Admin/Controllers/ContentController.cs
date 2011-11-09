using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nebo.Helpers;
using Nebo.Models;

namespace Nebo.Areas.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult Add(int? parentId)
        {
            ViewData["parentId"] = parentId;
            return View();
        }

        [HttpPost]
        public ActionResult Add(FormCollection form, HttpPostedFileBase fileUpload)
        {
            using (var context = new ContentStorage())
            {
                var content = new Content();
                TryUpdateModel(content, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "SortOrder",
                                                "SeoDescription",
                                                "SeoKeywords"
                                            });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.ContentLevel = 1;
                
                if(fileUpload!=null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);
                    content.ImageSource = fileName;
                }

                //context.AddToContent(content);

                //context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }

        /*
        public ActionResult Edit(int id)
        {

            return View();
        }*/


    }
}
