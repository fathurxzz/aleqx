using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewVision.UI.Models
{
    [MetadataType(typeof(MediaValidation))]
    partial class Media
    {

    }

    public class MediaValidation
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Фото")]
        public string ImageSrc { get; set; }

        [Display(Name = "Видео")]
        public string VideoSrc { get; set; }

        [Display(Name = "Порядок отображения")]
        public int SortOrder { get; set; }

        [Display(Name = "Видео или Фото?")]
        public int ContentType { get; set; }
    }
}