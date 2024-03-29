﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions.Graphics;

namespace Kulumu.Controllers
{
    public class CatalogueController : Controller
    {
        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        [HttpPost]
        public void UpdateProjectImage(string fileName)
        {
            GraphicsHelper.SaveCachedImage("~/Content/Images", fileName, SiteSettings.GetThumbnail("productPreview"));
        }

    }
}
