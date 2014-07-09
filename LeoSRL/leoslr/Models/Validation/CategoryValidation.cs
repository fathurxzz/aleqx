using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Leo.Models
{
    [MetadataType(typeof(CategoryValidation))]
    public partial class Category
    {
         
    }

    public class CategoryValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст страницы")]
        public string Text { get; set; }

        [Display(Name = "Порядок отображения")]
        [Required(ErrorMessage = "Введите порядок отображения")]
        public string SortOrder { get; set; }

        [Display(Name = "Идентификатор (является частью адреса в строке браузера)")]
        [Required(ErrorMessage = "Введите идентификатор")]
        public string Name { get; set; }

        [Display(Name = "Доступность на текущем языке")]
        public bool IsCorrectLang { get; set; }
    }
}