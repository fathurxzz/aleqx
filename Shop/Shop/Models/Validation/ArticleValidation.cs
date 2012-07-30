using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    [MetadataType(typeof(ArticleValidation))]
    public partial class Article
    {

    }

    public class ArticleValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Идентификатор")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Описание (для поисковиков)")]
        public string SeoDescription { get; set; }

        [DisplayName("Ключевые слова (для поисковиков)")]
        public string SeoKeywords { get; set; }

        [DisplayName("Опубликовать")]
        public bool Published { get; set; }

        [DisplayName("Содержимое")]
        public string Text { get; set; }
    }
}