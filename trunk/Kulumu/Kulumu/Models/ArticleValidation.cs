using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kulumu.Models
{
    [MetadataType(typeof(ArticleValidation))]
    public partial class Article
    {

    }
    public class ArticleValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Краткое описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        public string Text { get; set; }

    }
}