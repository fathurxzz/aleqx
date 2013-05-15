using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    public class CatalogueModel:SiteModel
    {
        public IEnumerable<Brand> Brands { get; set; }
        public Brand Brand { get; set; }

        public CatalogueModel(Language lang, SiteContainer context, string brandId) : base(lang, context, "gallery")
        {
            Title += " - Галерея брендов";

            if (brandId == null)
            {
                Brands = context.Brand.ToList();
                foreach (Brand brand in Brands)
                {
                    brand.CurrentLang = lang.Id;
                }
            }
            else
            {
                Brand = context.Brand.First(b => b.Name == brandId);
                Brand.CurrentLang = lang.Id;
                Title += " - " + Brand.Title;
            }

        }
    }
}