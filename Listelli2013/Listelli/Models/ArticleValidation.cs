using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Listelli.Models
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
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Опубликовать")]
        public bool Published { get; set; }
    }
}