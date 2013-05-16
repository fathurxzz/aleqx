using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    public partial class BrandItem
    {
        public string Text { get; set; }

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
                var currentLang = BrandItemLangs.FirstOrDefault(p => p.LanguageId == value);
                if (currentLang == null)
                {
                    IsCorrectLang = false;
                    var anyLang = BrandItemLangs.FirstOrDefault();
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
        private void SetLang(BrandItemLang postLang)
        {
            Text = postLang.Text;
        }
    }
}