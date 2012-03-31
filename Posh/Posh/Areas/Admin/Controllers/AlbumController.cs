using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Models;

namespace Posh.Areas.Admin.Controllers
{
    public class AlbumController : Controller
    {
        //
        // GET: /Admin/Album/

        public ActionResult Create()
        {
            return View(new Album());
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var album = new Album{ImageSource = ""};
                TryUpdateModel(album,
                               new[]
                                   {
                                       "Name",
                                       "Title",
                                       "SortOrder"
                                   });

                context.AddToAlbum(album);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { Area = "", id = "catalogue" });
            }
        }
    }
}
