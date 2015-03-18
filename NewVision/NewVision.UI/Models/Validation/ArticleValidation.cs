using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewVision.UI.Models
{
    [MetadataType(typeof(ArticleValidation))]
    partial class Article
    {

    }
    public class ArticleValidation
    {
        [Display(Name = "Дата")]
        public System.DateTime Date { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Размещение заголовка")]
        public int TitlePosition{ get; set; }

        [Display(Name = "Изображение")]
        public string ImageSrc { get; set; }
    }
}