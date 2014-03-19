using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mayka.Helpers;
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

            var product = new Product { Content = content };

            TryUpdateModel(product, new[] { "SortOrder" });

            product.Description = HttpUtility.HtmlDecode(model.Description);
            if (fileUpload != null)
            {
                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 140);
                fileUpload.SaveAs(filePath);
                product.PreviewImageSource = fileName;
            }


            _context.Product.Add(product);
            _context.SaveChanges();


            return RedirectToAction("Products", "Home", new { area = "", id = content.Name });
        }


        public ActionResult Edit(int id)
        {
            var product = _context.Product.First(p => p.Id == id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product model, HttpPostedFileBase fileUpload)
        {
            var content = _context.Content.First(c => c.Id == model.ContentId);

            var product = _context.Product.First(p => p.Id == model.Id);
            TryUpdateModel(product, new[] {"SortOrder"});
            product.Description = HttpUtility.HtmlDecode(model.Description);
            if (fileUpload != null)
            {
                ImageHelper.DeleteImage(product.PreviewImageSource);

                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 140);
                fileUpload.SaveAs(filePath);
                product.PreviewImageSource = fileName;
            }

            _context.SaveChanges();

            return RedirectToAction("Products", "Home", new { area = "", id = content.Name });
        }

        public ActionResult AddProductImages(int id)
        {
            var product = _context.Product.First(p => p.Id == id);
            var sortOrder = 0;
            if (product.ProductImages.Any())
            {
                sortOrder = product.ProductImages.Max(p => p.SortOrder) + 1;
            }

            return View(new ProductImage { ProductId = product.Id, SortOrder = sortOrder });
        }

        [HttpPost]
        public ActionResult AddProductImages(ProductImage model, HttpPostedFileBase fileUpload)
        {
            var product = _context.Product.First(p => p.Id == model.ProductId);
            var image = new ProductImage();
            TryUpdateModel(image, new[] { "SortOrder" });
            if (fileUpload != null)
            {
                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                //GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 140);
                fileUpload.SaveAs(filePath);
                image.ImageSource = fileName;
            }
            product.ProductImages.Add(image);
            _context.SaveChanges();
            return RedirectToAction("ProductDetails", "Home", new { area = "", id = product.Id });
        }

        public ActionResult DeleteProductImage(int id)
        {
            var image = _context.ProductImage.First(pi => pi.Id == id);
            var product = image.Product;
            ImageHelper.DeleteImage(image.ImageSource);
            _context.ProductImage.Remove(image);
            _context.SaveChanges();
            return RedirectToAction("ProductDetails", "Home", new { area = "", id = product.Id });
        }

        public ActionResult ChangeImageOrder(int productId, int imageId, string order)
        {
            var product = _context.Product.First(p => p.Id == productId);

            

            if (order == "down")
            {
                var image = product.ProductImages.First(i => i.Id == imageId);
                var nextImage = product.ProductImages.FirstOrDefault(i => i.SortOrder == image.SortOrder + 1);
                image.SortOrder += 1;
                if (nextImage != null)
                {
                    nextImage.SortOrder -= 1;
                }
            }
            else if (order == "up")
            {
                var image = product.ProductImages.First(i => i.Id == imageId);
                var nextImage = product.ProductImages.FirstOrDefault(i => i.SortOrder == image.SortOrder - 1);
                image.SortOrder -= 1;
                if (nextImage != null)
                {
                    nextImage.SortOrder += 1;
                }
            }

            //foreach (var image in product.ProductImages.OrderBy(p=>p.SortOrder))
            //{
                
            //}

            _context.SaveChanges();

            return RedirectToAction("ProductDetails", "Home", new { area = "", id = product.Id });
        }

    }
}
