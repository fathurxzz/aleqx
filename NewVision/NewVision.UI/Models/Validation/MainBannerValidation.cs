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
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSrc { get; set; }
    }
}