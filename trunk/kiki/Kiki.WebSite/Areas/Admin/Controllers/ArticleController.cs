using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;
using Kiki.WebSite.Helpers;
using Kiki.WebSite.Helpers.Graphics;

namespace Kiki.WebSite.Areas.Admin.Controllers
{
    public class ArticleController : AdminController
    {
        //
        // GET: /Admin/Article/

        public ArticleController(ISiteRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            var articles = _repository.GetArticles();
            return View(articles);
        }

        public ActionResult Create()
        {
            return View(new Article { Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(Article model)
        {
            try
            {
                model.Id = 0;
                var article = new Article
                {
                    Name = string.IsNullOrEmpty(model.Name)
                        ? SiteHelper.UpdatePageWebName(model.Name, model.Title)
                        : SiteHelper.UpdatePageWebName(model.Name),
                    Date = model.Date,
                    Title = model.Title,
                    Description = model.Description,
                    Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text)
                };

                var file = Request.Files[0];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    article.ImageSource = fileName;
                }
                else
                {
                    article.ImageSource = article.ImageSource ?? "";
                }

                _repository.AddArticle(article);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message + (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : "");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var article = _repository.GetArticle(id);
                return View(article);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Article model)
        {
            try
            {
                var article = _repository.GetArticle(model.Id);
                article.Name = SiteHelper.UpdatePageWebName(model.Name);
                TryUpdateModel(article, new[] { "Title", "Date", "Description"});

                article.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);

                var file = Request.Files[0];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    if (!string.IsNullOrEmpty(article.ImageSource))
                    {
                        ImageHelper.DeleteImage(article.ImageSource);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    article.ImageSource = fileName;
                }
                else
                {
                    article.ImageSource = article.ImageSource ?? "";
                }


                _repository.SaveArticle(article);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var article = _repository.GetArticle(id);
            return View(article);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteArticle(id, ImageHelper.DeleteImage);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }


    }
}
