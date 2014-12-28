using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    partial class Product
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoText { get; set; }
        public bool IsCorrectLang { get; protected set; }

        private int _currentLang;

        private IEnumerable<ProductLang> ProductLangsCached
        {
            get { return (ICollection<ProductLang>)Cache.Default.GetOrAdd("ProductLangsCached_" + Id, key => (object)ProductLangs.ToList()); }
        }

        public int CurrentLang
        {
            get
            {
                return _currentLang;
            }

            set
            {
                _currentLang = value;
                var currentLang = ProductLangsCached.FirstOrDefault(c => c.LanguageId == value);
                if (currentLang == null)
                {
                    IsCorrectLang = false;
                    var anyLang = ProductLangsCached.FirstOrDefault();
                    if (anyLang != null)
                    {
                        SetLang(anyLang);
                    }
                }
                else
                {
                    IsCorrectLang = true;
                    SetLang(currentLang);
                }
            }
        }

        private void SetLang(ProductLang entityLang)
        {
            Title = entityLang.Title;
            SeoDescription = entityLang.SeoDescription;
            SeoKeywords = entityLang.SeoKeywords;
            SeoText = entityLang.SeoText;
            Description = entityLang.Description;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
