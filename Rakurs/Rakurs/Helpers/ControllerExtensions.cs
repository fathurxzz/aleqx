using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Models;

namespace Rakurs.Helpers
{
    public static class ControllerExtensions
    {
        public static void SetSeoContent(this Controller controller, SiteViewModel model)
        {
            controller.ViewBag.Title = model.Title;
            controller.ViewBag.SeoDescription = model.SeoDescription;
            controller.ViewBag.SeoKeywords = model.SeoKeywords;
        }
    }
}