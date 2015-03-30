using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionIntention.UI.Models
{
    [MetadataType(typeof(ContentItemValidation))]
    partial class ContentItem
    {

    }
    public class ContentItemValidation
    {
        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSrc { get; set; }
    }
}