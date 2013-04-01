using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions.Graphics;

namespace Filimonov.Controllers
{
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
    public class CatalogueController : Controller
    {
        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        [HttpPost]
        public void UpdateProjectImage(string fileName)
        {
            GraphicsHelper.SaveCachedImage("~/Content/Images", fileName, SiteSettings.GetThumbnail("projectImage"));
        }
    }
}
