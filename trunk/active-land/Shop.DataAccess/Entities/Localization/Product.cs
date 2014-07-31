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

        public int CurrentLang
        {
            get
            {
                return _currentLang;
            }

            set
            {
                _currentLang = value;
                var currentLang = ProductLangs.FirstOrDefault(c => c.LanguageId == value);
                if (currentLang == null)
                {
                    IsCorrectLang = false;
                    var anyLang = ProductLangs.FirstOrDefault();
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
