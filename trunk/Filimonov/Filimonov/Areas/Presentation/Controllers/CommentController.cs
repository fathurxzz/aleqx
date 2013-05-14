using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;

namespace Filimonov.Areas.Presentation.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Presentation/Comment/

        public ActionResult Index(int id)
        {
            using (var context = new LibraryContainer())
            {
                var product = context.Product.Include("Comments").First(p => p.Id == id);
                return View(product);
            }
        }

        public ActionResult Create(int id)
        {
            using (var context = new LibraryContainer())
            {
                var product = context.Product.First(p => p.Id == id);
                ViewBag.productId = product.Id;
                //var comment = new Comment {Product = product};
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(int productId, FormCollection form)
        {
            using (var context = new LibraryContainer())
            {
                var product = context.Product.First(p => p.Id == productId);
                var comment = new Comment {Date = DateTime.Now};
                TryUpdateModel(comment, new[] {"Text"});

                var customer = context.Customer.First(c => c.Name == User.Identity.Name);
                
                comment.CustomerId = customer.Id;
                comment.CustomerName = customer.Name;
                comment.CustomerTitle = customer.Title;

                product.Comments.Add(comment);
                
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new LibraryContainer())
            {
                var comment = context.Comment.First(c => c.Id == id);
                return View(comment);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new LibraryContainer())
            {
                var comment = context.Comment.First(c => c.Id == id);
                TryUpdateModel(comment, new[] {"Text"});
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        public ActionResult Delete(int id)
        {
            using (var context = new LibraryContainer())
            {
                var comment = context.Comment.First(c => c.Id == id);
                context.DeleteObject(comment);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
