using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    [MetadataType(typeof(ArticleValidation))]
    partial class Article
    {

    }
    public class ArticleValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Дата")]
        [Required(ErrorMessage = "Введите дату")]
        public string Date { get; set; }

        [Display(Name = "Идентификатор")]
        [Required(ErrorMessage = "Введите идентификатор")]
        public string Name { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSource { get; set; }

        [Display(Name = "Доступность на текущем языке")]
        public bool IsCorrectLang { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Опубликована")]
        public bool IsActive { get; set; }
    }
}
