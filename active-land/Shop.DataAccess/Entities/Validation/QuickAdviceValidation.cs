using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    [MetadataType(typeof(QuickAdviceValidation))]
    partial class QuickAdvice
    {

    }

    class QuickAdviceValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Доступность на текущем языке")]
        public bool IsCorrectLang { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Порядок отображения")]
        public int SortOrder { get; set; }

        [Display(Name = "Опубликована")]
        public bool Active { get; set; }
    }
}
