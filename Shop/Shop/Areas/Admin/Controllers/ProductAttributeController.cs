using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductAttributeController : Controller
    {
        //
        // GET: /Admin/ProductAttribute/

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var productAttributes = context.ProductAttribute.Include("ProductAttributeValues").ToList();
                return View(productAttributes);
            }
        }

        public ActionResult Create()
        {
            return View(new ProductAttribute());
        }

        [HttpPost]
        public ActionResult Create(ProductAttribute model)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    TryUpdateModel(model, new[] { "Name", "SortOrder", "ValueType", "ShowInCommonView", "Static" });
                        /*
                    var productAttribute = new ProductAttribute
                    {
                        Name = form["Name"],
                        SortOrder = Convert.ToInt32(form["SortOrder"]),
                        ValueType = string.IsNullOrEmpty(form["ValueType"]) ? string.Empty : form["ValueType"]
                    };
                        
                    productAttribute.ShowInCommonView = Convert.ToBoolean(form["ShowInCommonView"] == "true,false" ? "true" : form["ShowInCommonView"]);
                    productAttribute.Static = Convert.ToBoolean(form["Static"] == "true,false" ? "true" : form["Static"]);
                    */

                    //context.AddToProductAttribute(productAttribute);
                    context.AddToProductAttribute(model);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ShopContainer())
            {
                var productAttribute = context.ProductAttribute.First(pa => pa.Id == id);
                return View(productAttribute);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ShopContainer())
            {
                var productAttribute = context.ProductAttribute.First(pa => pa.Id == id);
                try
                {
                    TryUpdateModel(productAttribute, new[] { "Name", "SortOrder", "ValueType", "ShowInCommonView" });
                    if (productAttribute.ValueType == null)
                        productAttribute.ValueType = string.Empty;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                }
                return View(productAttribute);
            }

        }

      

        public ActionResult Delete(int id)
        {
            using (var context = new ShopContainer())
            {
                try
                {
                    var attribute = context.ProductAttribute.Include("ProductAttributeValues").First(a => a.Id == id);
                    while (attribute.ProductAttributeValues.Any())
                    {
                        var pav = attribute.ProductAttributeValues.First();
                        pav.Products.Clear();
                        context.DeleteObject(pav);
                    }
                    attribute.Categories.Clear();
                    context.DeleteObject(attribute);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message +"<br/>"+ ex.InnerException.Message;
                    var productAttributes = context.ProductAttribute.Include("ProductAttributeValues").ToList();
                    return View("Index", productAttributes);
                }
            }
        }


        public ActionResult ProductAttributeValues(int id)
        {
            using (var context = new ShopContainer())
            {

                var productAttribute = context.ProductAttribute.Include("ProductAttributeValues").First(pa => pa.Id == id);
                return View(productAttribute);
            }
        }

        public ActionResult DeleteProductAttributeValue(int id, int productAttributeId)
        {
            using (var context = new ShopContainer())
            {
                var productAttributeValue = context.ProductAttributeValues.First(pav => pav.Id == id);
                context.DeleteObject(productAttributeValue);
                context.SaveChanges();
                return RedirectToAction("ProductAttributeValues", new { id = productAttributeId });
            }
        }

        public ActionResult AddProductAttributeValue(int id)
        {
            using (var context = new ShopContainer())
            {
                var productAttribute = context.ProductAttribute.First(pa => pa.Id == id);
                return View(productAttribute);
            }
        }

        [HttpPost]
        public ActionResult AddProductAttributeValue(int id, string attributeValue)
        {
            using (var context = new ShopContainer())
            {
                var productAttribute = context.ProductAttribute.First(pa => pa.Id == id);
                productAttribute.ProductAttributeValues.Add(new ProductAttributeValues { Value = attributeValue });
                context.SaveChanges();
                return RedirectToAction("ProductAttributeValues", new { id = id });
            }
        }
    }
}
