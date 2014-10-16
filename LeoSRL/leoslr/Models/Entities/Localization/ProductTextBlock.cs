using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leo.Models
{
    public partial class ProductTextBlock
    {
        public string Text { get; set; }
        public string Title { get; set; }
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
                var currentLang = ProductTextBlockLangs.FirstOrDefault(p => p.LanguageId == value);
                if (currentLang == null)
                {
                    IsCorrectLang = false;
                    var anyLang = ProductTextBlockLangs.FirstOrDefault();
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

        private void SetLang(ProductTextBlockLang postLang)
        {
            Text = postLang.Text;
            Title = postLang.Title;
        }
    }
}