using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionIntention.UI.Models
{
    [MetadataType(typeof(PostItemValidation))]
    partial class PostItem
    {

    }

    public class PostItemValidation
    {
        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSrc { get; set; }
    }
}