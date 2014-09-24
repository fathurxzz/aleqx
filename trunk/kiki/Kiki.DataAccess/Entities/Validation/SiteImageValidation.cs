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

        [Display(Name = "Изображение")]
        public string ImageSource { get; set; }


    }
}
