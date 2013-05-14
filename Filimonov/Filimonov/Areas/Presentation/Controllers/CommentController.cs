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
            return View();
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

    }
}
