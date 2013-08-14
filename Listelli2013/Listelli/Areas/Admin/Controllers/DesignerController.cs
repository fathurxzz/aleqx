using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Helpers;
using Listelli.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Listelli.Areas.Admin.Controllers
{
    public class DesignerController : Controller
    {
        //
        // GET: /Admin/Designer/

        public ActionResult Index()
        {
            using (var context = new PortfolioContainer())
            {
                var designers = context.Designer.ToList();
                return View(designers);
            }
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Designer model, HttpPostedFileBase fileUpload)
        {
            using (var context = new PortfolioContainer())
            {
                var designer = new Designer
                               {
                                   Name = SiteHelper.UpdatePageWebName(model.Name),
                                   Description = HttpUtility.HtmlDecode(model.Description)
                               };

                TryUpdateModel(designer, new[] { "DesignerName", "DesignerNameF" });


                if (fileUpload != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 500);
                    designer.ImageSource = fileName;
                }


                context.AddToDesigner(designer);

                context.SaveChanges();
                
            }

            return RedirectToAction("Index");
        }


    }
}
