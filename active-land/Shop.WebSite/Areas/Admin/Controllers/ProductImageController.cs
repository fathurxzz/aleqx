using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;
using Shop.WebSite.Helpers.Graphics;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class ProductImageController : AdminController
    {

        public ProductImageController(IShopRepository repository)
            : base(repository)
        {
        }

        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            _repository.LangId = CurrentLangId;
            var product = _repository.GetProduct(id);
            return View(product.ProductImages);
        }

        public ActionResult Create(int id)
        {
            _repository.LangId = CurrentLangId;
            return View(new ProductImage{ProductId = id});
        }

        [HttpPost]
        public ActionResult Create(ProductImage model)
        {
            _repository.LangId = CurrentLangId;
            var productId = model.ProductId;
            try
            {
                var product = _repository.GetProduct(productId);
                
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    var productImage = new ProductImage { ImageSource = fileName };
                    product.ProductImages.Add(productImage);
                }

                _repository.SaveProduct(product);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return RedirectToAction("Index", new {id = productId});
        }

        public ActionResult Delete(int id, int productId)
        {
            _repository.DeleteProductImage(id, ImageHelper.DeleteImage);
            return RedirectToAction("Index", new {id = productId});
        }
    }
}
