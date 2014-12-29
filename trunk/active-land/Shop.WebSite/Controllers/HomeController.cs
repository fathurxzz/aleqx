using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Shop.DataAccess;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;
using Shop.WebSite.Models;

namespace Shop.WebSite.Controllers
{
    public class HomeController : DefaultController
    {

        public HomeController(IShopRepository repository) : base(repository)
        {
        }

        public ActionResult Index(string id, string msg)
        {
            _repository.LangId = CurrentLangId;
            var model = new SiteModel(_repository, CurrentLangId, id, true) { CurrentLangCode = CurrentLangCode };
            this.SetSeoContent(model);
            if (msg == "thanks")
            {
                ViewBag.Message = "Спасибо, ваше сообщение отправлено";
            }
            return View(model);
        }

        public ActionResult Catalogue(string category, string filter, int? page, string sortOrder, string sortBy)
        {
            _repository.LangId = CurrentLangId;
            var model = new CatalogueModel(_repository,CurrentLangId, page, category, filter: filter,sortBy:sortBy,sortOrder:sortOrder)
            {
                CurrentLangCode = CurrentLangCode
            };
            ViewBag.ProductTotalCount = model.ProductTotalCount;
            ViewBag.Page = model.Page;
            ViewBag.SortOrder = sortOrder;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Search(string q, int? page)
        {
            _repository.LangId = CurrentLangId;
            var model = new CatalogueModel(_repository, CurrentLangId, page,query:q)
            {
                CurrentLangCode = CurrentLangCode
            };
            ViewBag.ProductTotalCount = model.ProductTotalCount;
            ViewBag.Page = model.Page;
            ViewBag.Q = q;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult ProductDetails(string product, string msg)
        {
            _repository.LangId = CurrentLangId;
            var model = new CatalogueModel(_repository, CurrentLangId, null, productName: product)
            {
                CurrentLangCode = CurrentLangCode
            };
            if (msg == "thanks")
            {
                ViewBag.Message = "Спасибо, ваше сообщение отправлено";
            }
            this.SetSeoContent(model);
            ViewBag.CurrentLangCode = CurrentLangCode;
            return View(model);
        }

        public ActionResult ArticleDetails(string article)
        {
            _repository.LangId = CurrentLangId;
            var model = new CatalogueModel(_repository, CurrentLangId, null, articleName: article)
            {
                CurrentLangCode = CurrentLangCode
            };
            this.SetSeoContent(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Feedback(FormCollection form)
        {
            var contentName = form["contentName"];


            var feedbackForm = new FeedbackForm
            {
                Name = form["customerName"],
                Phone = form["mobilePhone"],
                Email = form["email"],
                Question = form["question"]
            };

            MailHelper.Notify(feedbackForm);

            if (string.IsNullOrEmpty(contentName))
            {
                return RedirectToAction("ProductDetails", new { product = form["productName"], msg = "thanks" });
            }
            return RedirectToAction("Index", new { id = contentName, msg="thanks" });
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public string GetProducts(int id)
        {
            var result = new List<ProductSelectModel>();
            if (id == 0)
            {
                result.Add(new ProductSelectModel { id = 0, title = "Выберите категорию" });
            }
            else
            {
                var category = _repository.GetCategory(id);
                if (category.Products.Count == 0)
                {
                    result.Add(new ProductSelectModel { id = 0, title = "Ничего не найдено" });
                }
                else
                {
                    foreach (var product in category.Products.OrderBy(p => p.Title))
                    {
                        product.CurrentLang = CurrentLangId;
                        result.Add(new ProductSelectModel {id = product.Id, title = product.Title});
                    }
                }
            }

            return JsonConvert.SerializeObject(result.OrderBy(r=>r.title));
        }


        public ActionResult Test()
        {
            Cache.Default.Clear();
            _repository.LangId = CurrentLangId;
            var categories = _repository.GetCategories();
            return View();
        }
    }
}
