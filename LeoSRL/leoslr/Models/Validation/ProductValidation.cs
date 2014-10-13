using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Leo.Models
{
    [MetadataType(typeof(ProductValidation))]
    public partial class Product
    {

    }

    public class ProductValidation
    {
        [Display(Name = "Идентификатор (является частью адреса в строке браузера, вводить латиницей!!!)")]
        [Required(ErrorMessage = "Введите идентификатор")]
        public string Name { get; set; }

        [Display(Name = "Доступность на текущем языке")]
        public bool IsCorrectLang { get; set; }
        
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст страницы")]
        [Required(ErrorMessage = "Введите текст")]
        public string Text { get; set; }

        [Display(Name = "Порядок отображения")]
        [Required(ErrorMessage = "Введите порядок отображения")]
        public int SortOrder { get; set; }

        [Display(Name = "Страница контента")]
        public bool IsContentPage { get; set; }


    }
}