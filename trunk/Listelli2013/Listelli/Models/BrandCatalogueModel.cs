using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Listelli.Models
{
    public class BrandCatalogueModel:SiteModel
    {
        public Brand Brand { get; set; }
        public IEnumerable<Brand> Brands { get; set; }

        public BrandGroup BrandGroup { get; set; }
        public IEnumerable<BrandGroup> BrandGroups { get; set; }


        public BrandCatalogueModel(Language lang, SiteContainer context,string brandGroupId, string brandId) : base(lang, context, "gallery")
        {
            Title += " - Галерея брендов";

            if (brandGroupId == null)
            {
                BrandGroups = context.BrandGroup.ToList();
                foreach (BrandGroup bg in BrandGroups)
                {
                    bg.CurrentLang = lang.Id;
                }
            }
            else
            {
                BrandGroup = context.BrandGroup.Include("BrandGroupItems").Include("Brands").FirstOrDefault(b => b.Name == brandGroupId);

                if (BrandGroup == null)
                {
                    throw new ObjectNotFoundException(string.Format("Бренд {0} не найден", brandGroupId));
                }

                BrandGroup.CurrentLang = lang.Id;

                foreach (var item in BrandGroup.BrandGroupItems)
                {
                    item.CurrentLang = lang.Id;
                }


                if (brandId == null)
                {
                    foreach (var item in BrandGroup.Brands)
                    {
                        item.CurrentLang = lang.Id;
                    }
                    Title += " - " + BrandGroup.Title;
                    Brands = BrandGroup.Brands;
                }

                if (brandId == null)
                {
                    //Brands = context.Brand.ToList();
                    //foreach (Brand brand in Brands)
                    //{
                    //    brand.CurrentLang = lang.Id;
                    //}
                }
                else
                {
                    Brand = context.Brand.Include("BrandItems").FirstOrDefault(b => b.Name == brandId);

                    if (Brand == null)
                    {
                        throw new ObjectNotFoundException(string.Format("Элемент бренда {0} не найден", brandId));
                    }

                    Brand.CurrentLang = lang.Id;

                    foreach (var item in Brand.BrandItems)
                    {
                        item.CurrentLang = lang.Id;
                        if (item.ContentType == 3)
                        {
                            item.BrandItemImages.Load();
                        }
                    }
                    Title += " - " + Brand.Title;
                }
            }



            

        }
    }
}