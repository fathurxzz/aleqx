using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewVision.UI.Models
{
    [MetadataType(typeof(ContentValidation))]
    partial class Content
    {

    }

    public class ContentValidation
    {
        [Display(Name = "Заголовок пункта меню")]
        public string MenuTitle { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Фото")]
        public string ImageSrc { get; set; }

        [Display(Name = "Порядок отображения")]
        public string SortOrder { get; set; }
    }
}