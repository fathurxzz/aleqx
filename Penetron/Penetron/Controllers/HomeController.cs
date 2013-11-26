using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Penetron.Models;
using SiteExtensions;

namespace Penetron.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiteContext _context;

        public HomeController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var model = new SiteModel(_context, null);
            this.SetSeoContent(model);
            ViewBag.IsHomePage = true;
            return View(model);
        }


        public ActionResult Technologies(string categoryId, string subCategoryId)
        {
            var model = new TechnologyModel(_context, categoryId, subCategoryId);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("Technologies", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = 0;
            ViewBag.CategoryLevel = model.Technology.CategoryLevel == 0 ? "technologyRoot" : "technology";
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult SiteContent(string id)
        {
            var model = new SiteModel(_context, id);
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.CurrentPage = model.Content.Name;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Buildings(string categoryId, string subCategoryId)
        {
            var model = new BuildingModel(_context, categoryId, subCategoryId, (int)EContentType.Buildings);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("Buildings", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = (int)EContentType.Buildings;
            ViewBag.CategoryLevel = model.Building.CategoryLevel == 0 ? "buildingRoot" : "building";
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Products(string categoryId, string subCategoryId)
        {
            var model = new BuildingModel(_context, categoryId, subCategoryId, (int)EContentType.Products);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("Products", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = (int)EContentType.Products;
            ViewBag.CategoryLevel = model.Building.CategoryLevel == 0 ? "productRoot" : "product";
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Documents(string categoryId, string subCategoryId)
        {
            var model = new BuildingModel(_context, categoryId, subCategoryId, (int)EContentType.Documents);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("Documents", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = (int)EContentType.Documents;
            ViewBag.CategoryLevel = model.Building.CategoryLevel == 0 ? "documentRoot" : "document";
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult WhereToBuy(string categoryId, string subCategoryId)
        {
            var model = new BuildingModel(_context, categoryId, subCategoryId, (int)EContentType.WhereToBuy);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("WhereToBuy", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = (int)EContentType.WhereToBuy;
            ViewBag.CategoryLevel = model.Building.CategoryLevel == 0 ? "wheretobuyRoot" : "wheretobuy";
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult About(string categoryId, string subCategoryId)
        {
            var model = new BuildingModel(_context, categoryId, subCategoryId, (int)EContentType.About);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("About", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = (int)EContentType.About;
            ViewBag.CategoryLevel = model.Building.CategoryLevel == 0 ? "aboutRoot" : "about";
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            model.Articles = _context.Article.OrderBy(a => a.Date).ToList();
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Articles(string id)
        {
            var model = new BuildingModel(_context, null, null, (int)EContentType.About, id);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("About", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = (int)EContentType.About;
            ViewBag.CategoryLevel = "about";
            ViewBag.CategoryId = "";
            ViewBag.SubCategoryId = "";
            ViewBag.ArticleId = model.Article != null ? model.Article.Name : "";
            model.Articles = _context.Article.OrderBy(a => a.Date).ToList();
            this.SetSeoContent(model);
            return View(model);
        }


        public ActionResult UserArticle(string id)
        {
            var article = _context.UserArticle.First(a => a.Name == id);
            return View(article);
        }


        public ActionResult Search(string q)
        {
            if (string.IsNullOrEmpty(q))
                return View(new List<SearchResultModel>());

            var result = new List<SearchResultModel>();


            var technologies = _context.Technology.Include("Parent").Where(t => t.Active).ToList();
            foreach (var item in technologies)
            {
                if (item.Title.ToLower().Contains(q.ToLower()))
                {
                    var resultItem = new SearchResultModel { Title = item.Title, ControllerName = "Home", ActionName = "Technologies", Map = new List<SearchResultModel>() };
                    
                    resultItem.Map.Add(new SearchResultModel { Title = "Технологии", ActionName = resultItem.ActionName, ControllerName = resultItem.ControllerName, CategoryId = null, SubCategoryId = null });
                    

                    if (item.Parent != null)
                    {
                        resultItem.CategoryId = item.Parent.Name;
                        resultItem.SubCategoryId = item.Name;
                        resultItem.Map.Add(new SearchResultModel { Title = item.Parent.Title, ActionName = resultItem.ActionName, ControllerName = resultItem.ControllerName, CategoryId = item.Parent.Name, SubCategoryId = null });
                    }
                    else
                    {
                        resultItem.CategoryId = item.Name;
                        resultItem.SubCategoryId = null;
                    }

                    result.Add(resultItem);
                }
            }



            var products = _context.Building.Include("Parent").Where(b => b.ContentType == (int)EContentType.Products && b.Active).ToList();
            foreach (var item in products)
            {
                if (item.Title.ToLower().Contains(q.ToLower()))
                {
                    var resultItem = new SearchResultModel { Title = item.Title, ControllerName = "Home", ActionName = "Products", Map = new List<SearchResultModel>() };
                    resultItem.Map.Add(new SearchResultModel { Title = "Продукция", ActionName = resultItem.ActionName, ControllerName = resultItem.ControllerName, CategoryId = null, SubCategoryId = null });
                    if (item.Parent != null)
                    {
                        resultItem.CategoryId = item.Parent.Name;
                        resultItem.SubCategoryId = item.Name;
                        resultItem.Map.Add(new SearchResultModel{Title = item.Parent.Title, ActionName = resultItem.ActionName,ControllerName = resultItem.ControllerName,CategoryId = item.Parent.Name, SubCategoryId = null});
                    }
                    else
                    {
                        resultItem.CategoryId = item.Name;
                        resultItem.SubCategoryId = null;
                    }

                    result.Add(resultItem);
                }
            }


            var documents = _context.Building.Include("Parent").Where(b => b.ContentType == (int)EContentType.Documents && b.Active).ToList();
            foreach (var item in documents)
            {
                if (item.Title.ToLower().Contains(q.ToLower()))
                {
                    var resultItem = new SearchResultModel { Title = item.Title, ControllerName = "Home", ActionName = "Documents", Map = new List<SearchResultModel>() };
                    resultItem.Map.Add(new SearchResultModel { Title = "Статьи и документация", ActionName = resultItem.ActionName, ControllerName = resultItem.ControllerName, CategoryId = null, SubCategoryId = null });
                    if (item.Parent != null)
                    {
                        resultItem.CategoryId = item.Parent.Name;
                        resultItem.SubCategoryId = item.Name;
                        resultItem.Map.Add(new SearchResultModel { Title = item.Parent.Title, ActionName = resultItem.ActionName, ControllerName = resultItem.ControllerName, CategoryId = item.Parent.Name, SubCategoryId = null });
                    }
                    else
                    {
                        resultItem.CategoryId = item.Name;
                        resultItem.SubCategoryId = null;
                    }

                    result.Add(resultItem);
                }
            }

            var buildings = _context.Building.Include("Parent").Where(b => b.ContentType == (int)EContentType.Buildings && b.Active).ToList();
            foreach (var item in buildings)
            {
                if (item.Title.ToLower().Contains(q.ToLower()))
                {
                    var resultItem = new SearchResultModel { Title = item.Title, ControllerName = "Home", ActionName = "Buildings", Map = new List<SearchResultModel>() };
                    resultItem.Map.Add(new SearchResultModel { Title = "Объекты и решения", ActionName = resultItem.ActionName, ControllerName = resultItem.ControllerName, CategoryId = null, SubCategoryId = null });
                    if (item.Parent != null)
                    {
                        resultItem.CategoryId = item.Parent.Name;
                        resultItem.SubCategoryId = item.Name;
                        resultItem.Map.Add(new SearchResultModel { Title = item.Parent.Title, ActionName = resultItem.ActionName, ControllerName = resultItem.ControllerName, CategoryId = item.Parent.Name, SubCategoryId = null });
                    }
                    else
                    {
                        resultItem.CategoryId = item.Name;
                        resultItem.SubCategoryId = null;
                    }

                    result.Add(resultItem);
                }
            }


            var wheretobuy = _context.Building.Include("Parent").Where(b => b.ContentType == (int)EContentType.WhereToBuy && b.Active).ToList();
            foreach (var item in wheretobuy)
            {
                if (item.Title.ToLower().Contains(q.ToLower()))
                {
                    var resultItem = new SearchResultModel { Title = item.Title, ControllerName = "Home", ActionName = "WhereToBuy", Map = new List<SearchResultModel>() };
                    resultItem.Map.Add(new SearchResultModel { Title = "Где купить", ActionName = resultItem.ActionName, ControllerName = resultItem.ControllerName, CategoryId = null, SubCategoryId = null });
                    if (item.Parent != null)
                    {
                        resultItem.CategoryId = item.Parent.Name;
                        resultItem.SubCategoryId = item.Name;
                        resultItem.Map.Add(new SearchResultModel { Title = item.Parent.Title, ActionName = resultItem.ActionName, ControllerName = resultItem.ControllerName, CategoryId = item.Parent.Name, SubCategoryId = null });
                    }
                    else
                    {
                        resultItem.CategoryId = item.Name;
                        resultItem.SubCategoryId = null;
                    }

                    result.Add(resultItem);
                }
            }

            var about = _context.Building.Include("Parent").Where(b => b.ContentType == (int)EContentType.About && b.Active).ToList();
            foreach (var item in about)
            {
                if (item.Title.ToLower().Contains(q.ToLower()))
                {
                    var resultItem = new SearchResultModel { Title = item.Title, ControllerName = "Home", ActionName = "About", Map = new List<SearchResultModel>() };
                    resultItem.Map.Add(new SearchResultModel { Title = "О компании", ActionName = resultItem.ActionName, ControllerName = resultItem.ControllerName, CategoryId = null, SubCategoryId = null });
                    if (item.Parent != null)
                    {
                        resultItem.CategoryId = item.Parent.Name;
                        resultItem.SubCategoryId = item.Name;
                        resultItem.Map.Add(new SearchResultModel { Title = item.Parent.Title, ActionName = resultItem.ActionName, ControllerName = resultItem.ControllerName, CategoryId = item.Parent.Name, SubCategoryId = null });
                    }
                    else
                    {
                        resultItem.CategoryId = item.Name;
                        resultItem.SubCategoryId = null;
                    }

                    result.Add(resultItem);
                }
            }

            return View(result);
        }



    }
}
