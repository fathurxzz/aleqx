using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class ProductStockController : AdminController
    {
        public ProductStockController(IShopRepository repository) : base(repository)
        {

        }

        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            _repository.LangId = CurrentLangId;
            var product = _repository.GetProduct(id);
            return View(product.ProductStocks);
        }

        public ActionResult Create(int id)
        {
            _repository.LangId = CurrentLangId;
            return View(new ProductStock { ProductId = id });
        }

        [HttpPost]
        public ActionResult Create(ProductStock model)
        {
            _repository.LangId = CurrentLangId;
            var productId = model.ProductId;
            try
            {
                var product = _repository.GetProduct(productId);

                ProductStock stock = new ProductStock
                {
                    Color = model.Color,
                    Size = model.Size,
                    IsAvailable = model.IsAvailable,
                    StockNumber = model.StockNumber
                };

                product.ProductStocks.Add(stock);

                _repository.SaveProduct(product);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return RedirectToAction("Index", new { id = productId });
        }

        public ActionResult Edit(int id)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                var productStock = _repository.GetProductStock(id);
                return View(productStock);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(new ProductStock());
        }

        [HttpPost]
        public ActionResult Edit(ProductStock model)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                var productStock = _repository.GetProductStock(model.Id);
                TryUpdateModel(productStock, new[] { "StockNumber", "Size", "Color", "IsAvailable"});
                _repository.SaveProductStock(productStock);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id, int productId)
        {
            _repository.DeleteProductStock(id);
            return RedirectToAction("Index", new { id = productId });
        }

    }
}
