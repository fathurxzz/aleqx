using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewVision.UI.Models
{
    [MetadataType(typeof(TagValidation))]
    partial class Tag
    {

    }
    public class TagValidation
    {
        [Display(Name = "Заголовок RU")]
        public string Title { get; set; }

        [Display(Name = "Заголовок EN")]
        public string TitleEn { get; set; }

        [Display(Name = "Заголовок UA")]
        public string TitleUa { get; set; }
        
    }
}