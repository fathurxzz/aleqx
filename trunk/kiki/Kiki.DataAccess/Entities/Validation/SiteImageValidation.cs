using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiki.DataAccess.Entities
{
    [MetadataType(typeof(SiteImageValidation))]
    partial class SiteImage
    {

    }

    class SiteImageValidation
    {
        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Текст (ENG)")]
        public string TextEng { get; set; }

        [Display(Name = "Дополнительный текст")]
        public string Text2 { get; set; }

        [Display(Name = "Дополнительный текст (ENG)")]
        public string Text2Eng { get; set; }

        [Display(Name = "Текст слева на изображении")]
        public string Text0 { get; set; }

        [Display(Name = "Текст слева на изображении (ENG)")]
        public string Text0Eng { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSource { get; set; }


    }
}
