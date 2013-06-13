using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ego.Models;
using SiteExtensions.Graphics;

namespace Ego.Controllers
{
    public class CatalogueController : Controller
    {
        private string _descr = "Все майки - 100% коттон, индивидуальный пошив и шелкографическое нанесение.";

        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        [HttpPost]
        public string UpdateProjectImage(int productId)
        {
            using (var context = new SiteContainer())
            {
                var product = context.Product.First(p => p.Id == productId);
                GraphicsHelper.SaveCachedImage("~/Content/Images", product.ImageSource, SiteSettings.GetThumbnail("product"));
                return _descr + product.Description;
            }
        }

    }
}
