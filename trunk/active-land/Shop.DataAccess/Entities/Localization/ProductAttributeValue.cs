using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    partial class ProductAttributeValue
    {
        public string Title { get; set; }
        
        public bool IsCorrectLang { get; protected set; }

        private int _currentLang;

        private IEnumerable<ProductAttributeValueLang> ProductAttributeValueLangsCached
        {
            get
            {
                return (ICollection<ProductAttributeValueLang>)Cache.Default.GetOrAdd("ProductAttributeValueLang_" + Id, key => (object)ProductAttributeValueLangs.ToList());
            }
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
                var currentLang = ProductAttributeValueLangsCached.FirstOrDefault(c => c.LanguageId == value);
                if (currentLang == null)
                {
                    IsCorrectLang = false;
                    var anyLang = ProductAttributeValueLangsCached.FirstOrDefault();
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

        private void SetLang(ProductAttributeValueLang entityLang)
        {
            Title = entityLang.Title;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
