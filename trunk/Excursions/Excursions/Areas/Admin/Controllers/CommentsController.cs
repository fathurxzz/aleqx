using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Models;

namespace Excursions.Areas.Admin.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        //
        // GET: /Admin/Comments/

        public ActionResult Delete(int id, string excursionId)
        {

            using (var context = new ContentStorage())
            {
                var comment = context.Comments.Select(c => c).Where(c => c.Id == id).First();
                context.DeleteObject(comment);
                context.SaveChanges();
            }

            return RedirectToAction("Details", "Excursions", new { area = "", id = excursionId });
        }

    }
}
