using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Leo.Models
{

    [MetadataType(typeof(ProductTextBlockValidation))]
    public partial class ProductTextBlock
    {

    }

    public class ProductTextBlockValidation
    {
        [Display(Name = "Доступность на текущем языке")]
        public bool IsCorrectLang { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст страницы")]
        public string Text { get; set; }

        [Display(Name = "Порядок отображения")]
        [Required(ErrorMessage = "Введите порядок отображения")]
        public int SortOrder { get; set; }
    }
}