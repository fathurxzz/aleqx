using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewVision.UI.Models
{
    [MetadataType(typeof(MainBannerValidation))]
    partial class MainBanner
    {

    }

    public class MainBannerValidation
    {
        [Display(Name = "Заголовок RU")]
        public string Title { get; set; }

        [Display(Name = "Заголовок EN")]
        public string TitleEn { get; set; }

        [Display(Name = "Заголовок UA")]
        public string TitleUa { get; set; }

        [Display(Name = "Описание RU")]
        public string Description { get; set; }

        [Display(Name = "Описание EN")]
        public string DescriptionEn { get; set; }

        [Display(Name = "Описание UA")]
        public string DescriptionUa { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSrc { get; set; }
    }
}