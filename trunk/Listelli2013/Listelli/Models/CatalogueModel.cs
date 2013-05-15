using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    public class CatalogueModel:SiteModel
    {
        public IEnumerable<Brand> Brands { get; set; }

        public CatalogueModel(Language lang, SiteContainer context) : base(lang, context, "gallery")
        {
            Brands = context.Brand.ToList();
        }
    }
}