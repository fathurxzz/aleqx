using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    partial class ProductAttribute
    {
        public string Title { get; set; }
        public string UnitTitle { get; set; }
        public bool IsCorrectLang { get; protected set; }


        private int _currentLang;

        private IEnumerable<ProductAttributeLang> ProductAttributeLangsCached
        {
            get { return (ICollection<ProductAttributeLang>)Cache.Default.GetOrAdd("ProductAttributeLang_" + Id, key => (object)ProductAttributeLangs.ToList()); }
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
                var currentLang = ProductAttributeLangsCached.FirstOrDefault(c => c.LanguageId == value);
                if (currentLang == null)
                {
                    IsCorrectLang = false;
                    var anyLang = ProductAttributeLangsCached.FirstOrDefault();
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

        private void SetLang(ProductAttributeLang entityLang)
        {
            Title = entityLang.Title;
            UnitTitle = entityLang.UnitTitle;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
