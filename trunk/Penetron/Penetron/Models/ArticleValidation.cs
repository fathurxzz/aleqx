using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Penetron.Models
{
    [MetadataType(typeof(ArticleValidation))]
    public partial class Article
    {

    }
    public class ArticleValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Веб-имя страницы (отображается в строке браузера)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }

        [DisplayName("Опубликовать")]
        public bool Published { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

    }
}