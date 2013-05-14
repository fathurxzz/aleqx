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

                product.Comments.Add(comment);
                
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

    }
}
