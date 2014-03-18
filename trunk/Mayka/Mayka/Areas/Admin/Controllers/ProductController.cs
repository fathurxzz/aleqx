using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mayka.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Mayka.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly SiteContext _context;

        public ProductController(SiteContext context)
        {
            _context = context;
        }



        public ActionResult Create(int id)
        {
            var content = _context.Content.First(c => c.Id == id);
            return View(new Product { Content = content, ContentId = id });
        }

        [HttpPost]
        public ActionResult Create(Product model, HttpPostedFileBase fileUpload)
        {
            var content = _context.Content.First(c => c.Id == model.ContentId);

            var product = new Product{Content = content};
            product.Description =  HttpUtility.HtmlDecode(model.Description);
            if (fileUpload != null)
            {
                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 140);
                //fileUpload.SaveAs(filePath);
                product.PreviewImageSource = fileName;
            }

            
            _context.Product.Add(product);
            _context.SaveChanges();


            return RedirectToAction("Products", "Home", new { area = "", id = content.Name });
        }

    }
}
