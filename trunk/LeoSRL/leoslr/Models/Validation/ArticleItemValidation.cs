using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Leo.Models
{
    [MetadataType(typeof(ArticleItemValidation))]
    public partial class ArticleItem
    {
         
    }

    public class ArticleItemValidation
    {
        [Display(Name = "Текст страницы")]
        public string Text { get; set; }

        [Display(Name = "Порядок отображения")]
        [Required(ErrorMessage = "Введите порядок отображения")]
        public string SortOrder { get; set; }
    }
}