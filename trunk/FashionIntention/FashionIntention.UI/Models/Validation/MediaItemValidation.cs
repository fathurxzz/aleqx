using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionIntention.UI.Models
{
    [MetadataType(typeof(MediaItemValidation))]
    partial class MediaItem
    {

    }

    public class MediaItemValidation
    {
        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSrc { get; set; }

        [Display(Name = "Адрес видеоролика")]
        public string VideoSrc { get; set; }
    }
}