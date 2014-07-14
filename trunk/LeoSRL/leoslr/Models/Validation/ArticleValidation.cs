using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Leo.Models
{
    [MetadataType(typeof(ArticleValidation))]
    public partial class Article
    {
         
    }

    public class ArticleValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Краткое описание")]
        [Required(ErrorMessage = "Введите краткое описание")]
        public string Description { get; set; }

        [Display(Name = "Новость опубликована")]
        public bool Published { get; set; }
    }
}