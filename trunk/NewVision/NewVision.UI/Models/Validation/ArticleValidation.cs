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

        [Display(Name = "Заголовок RU")]
        public string Title { get; set; }

        [Display(Name = "Заголовок EN")]
        public string TitleEn { get; set; }
        
        [Display(Name = "Заголовок UA")]
        public string TitleUa { get; set; }

        [Display(Name = "Текст RU")]
        public string Text { get; set; }

        [Display(Name = "Текст EN")]
        public string TextEn { get; set; }

        [Display(Name = "Текст UA")]
        public string TextUa { get; set; }

        [Display(Name = "Размещение заголовка")]
        public int TitlePosition{ get; set; }

        [Display(Name = "Изображение")]
        public string ImageSrc { get; set; }

        [Display(Name = "Загрузка в ленту картинок")]
        public string ArticleImages { get; set; }

        [Display(Name = "Адрес видеоролика")]
        public string VideoSrc { get; set; }
    }
}