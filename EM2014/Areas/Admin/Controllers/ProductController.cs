using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2014.Helpers;
using EM2014.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace EM2014.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly SiteContext _context;

        public ProductController(SiteContext context)
        {
            _context = context;
        }
        public ActionResult Create(int id)
        {
            var content = _context.Contents.First(c => c.Id == id);

            var sortOrder = 0;
            if (content.Products.Any())
            {
                sortOrder = content.Products.Max(p => p.SortOrder) + 1;
            }

            //return View(new ProductImage { ProductId = product.Id, SortOrder = sortOrder });
            return View(new Product { Content = content, ContentId = id, SortOrder = sortOrder });
        }

        [HttpPost]
        public ActionResult Create(Product model, HttpPostedFileBase fileUpload)
        {
            var content = _context.Contents.First(c => c.Id == model.ContentId);

            var product = new Product { Content = content };
            TryUpdateModel(product, new[] { "SortOrder", "Title", "Name" });
            product.Text = HttpUtility.HtmlDecode(model.Text);
            if (fileUpload == null)
            {
                return View(model);
            }

            string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
            string filePath = Server.MapPath("~/Content/Images");
            filePath = Path.Combine(filePath, fileName);
            GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 305);
            //fileUpload.SaveAs(filePath);
            product.ImageSource = fileName;

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult Edit(int id)
        {
            var product = _context.Products.First(p => p.Id == id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product model, HttpPostedFileBase fileUpload)
        {
            var content = _context.Contents.First(c => c.Id == model.ContentId);

            var product = _context.Products.First(p => p.Id == model.Id);
            TryUpdateModel(product, new[] { "SortOrder", "Title", "Name" });
            product.Text = HttpUtility.HtmlDecode(model.Text);
            if (fileUpload != null)
            {
                ImageHelper.DeleteImage(product.ImageSource);

                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 305);
                //fileUpload.SaveAs(filePath);
                product.ImageSource = fileName;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "", category = content.Name, product = "" });
        }

        public ActionResult Delete(int id)
        {
            var product = _context.Products.First(p => p.Id == id);
            var content = product.Content;

            while (product.ProductItems.Any())
            {
                var productItem = product.ProductItems.First();
                if (string.IsNullOrEmpty(productItem.ImageSource))
                    ImageHelper.DeleteImage(productItem.ImageSource);
                _context.ProductItems.Remove(productItem);
            }

            ImageHelper.DeleteImage(product.ImageSource);

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
        }


        public ActionResult AddProductItem(int id)
        {
            var product = _context.Products.First(p => p.Id == id);
            var sortOrder = 0;
            if (product.ProductItems.Any())
            {
                sortOrder = product.ProductItems.Max(p => p.SortOrder) + 1;
            }
            return View(new ProductItem { Product = product, ProductId = product.Id, SortOrder = sortOrder });
        }

        [HttpPost]
        public ActionResult AddProductItem(ProductItem model, HttpPostedFileBase fileUpload)
        {
            var product = _context.Products.First(c => c.Id == model.ProductId);
            var productItem = new ProductItem { Product = product };
            TryUpdateModel(productItem, new[] { "SortOrder" });
            productItem.Text = HttpUtility.HtmlDecode(model.Text);
            if (fileUpload != null)
            {
                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 2000);
                //fileUpload.SaveAs(filePath);
                productItem.ImageSource = fileName;
            }

            _context.ProductItems.Add(productItem);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "", category = product.Content.Name, product = product.Name });

        }

        public ActionResult EditProductItem(int id)
        {
            return View(_context.ProductItems.First(p => p.Id == id));
        }

        [HttpPost]
        public ActionResult EditProductItem(ProductItem model, HttpPostedFileBase fileUpload)
        {
            var productItem = _context.ProductItems.First(p => p.Id == model.Id);
            TryUpdateModel(productItem, new[] { "SortOrder" });
            productItem.Text = HttpUtility.HtmlDecode(model.Text);
            if (fileUpload != null)
            {
                ImageHelper.DeleteImage(productItem.ImageSource);

                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 2000);
                //fileUpload.SaveAs(filePath);
                productItem.ImageSource = fileName;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "", category = productItem.Product.Content.Name, product = productItem.Product.Name });
        }

        public ActionResult DeleteProductItem(int id)
        {
            var productItem = _context.ProductItems.First(p => p.Id == id);
            var product = productItem.Product;
            if (!string.IsNullOrEmpty(productItem.ImageSource))
                ImageHelper.DeleteImage(productItem.ImageSource);

            _context.ProductItems.Remove(productItem);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "", category = product.Content.Name, product = product.Name });
        }


    }
}
