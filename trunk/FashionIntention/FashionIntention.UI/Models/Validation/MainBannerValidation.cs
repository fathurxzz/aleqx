using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionIntention.UI.Models
{

    [MetadataType(typeof(MainBannerValidation))]
    partial class MainBanner
    {

    }

    public class MainBannerValidation
    {
        [Display(Name = "Адрес ссылки")]
        public string Url { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSrc { get; set; }
    }
}