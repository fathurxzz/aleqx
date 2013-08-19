using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Listelli.App_LocalResources;
using Listelli.Controllers;
using Listelli.Models;

namespace Listelli.Areas.DesignersPortfolio.Controllers
{
    public class DesignerController : DefaultController
    {

        public ActionResult Details(string id)
        {
            using (var context = new PortfolioContainer())
            {
                var designer = context.Designer.FirstOrDefault(d => d.Name == id);
                if (designer == null)
                    return RedirectToAction("NotFoundPage", "Error",new {area=""});
                    //throw new ObjectNotFoundException("designer not found");
                return View(designer);
            }
        }

        public ActionResult RoomDetails(string id, int roomType)
        {
            using (var context = new PortfolioContainer())
            {
                var designer = context.Designer.Include("DesignerContents").First(d => d.Name == id);
                ViewBag.RoomType = roomType;
                foreach (var content in designer.DesignerContents.Where(c => c.RoomType == roomType))
                {
                    content.DesignerContantImages.Load();
                }
                return View(designer);
            }
        }

        public ActionResult LogOn(string id)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(id, "cde32wsx"))
                {
                    //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    FormsAuthentication.SetAuthCookie(id, false);

                    return RedirectToAction("Details", new { id = id });
                }
                else
                {
                    ModelState.AddModelError("", GlobalRes.WrongLoginOrPassword);
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult LogOff(string id)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Details", new { id = id });
        }

    }
}
