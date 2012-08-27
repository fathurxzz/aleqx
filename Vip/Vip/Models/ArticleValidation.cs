using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vip.Models
{
    [MetadataType(typeof(ArticleValidation))]
    public partial class Article
    {

    }

    public class ArticleValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Дата")]
        public string Date { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }
    }
}