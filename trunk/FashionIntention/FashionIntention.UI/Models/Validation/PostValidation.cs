using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionIntention.UI.Models
{
    [MetadataType(typeof(PostValidation))]
    partial class Post
    {

    }

    public class PostValidation
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSrc { get; set; }

        [Display(Name = "Опубликовано")]
        public bool Published { get; set; }
    }
}