using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    [MetadataType(typeof(ContentValidation))]
    partial class Content
    {

    }

    public class ContentValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        //[Display(Name = "Порядок отображения")]
        //[Required(ErrorMessage = "Введите порядок отображения")]
        //public string SortOrder { get; set; }

        [Display(Name = "Идентификатор")]
        public string Name { get; set; }

        [Display(Name = "Баннер")]
        public string ImageSource { get; set; }

        [Display(Name = "Доступность на текущем языке")]
        public bool IsCorrectLang { get; set; }

        [Display(Name = "Тип страницы")]
        public bool ContentType { get; set; }

        [Display(Name = "Порядок отображения")]
        public int SortOrder { get; set; }

        [Display(Name = "Описание для поисковиков")]
        public string SeoDescription { get; set; }

        [Display(Name = "Ключевые слова для поисковиков")]
        public string SeoKeywords { get; set; }

        [Display(Name = "Текст для поисковиков")]
        public string SeoText { get; set; }
     
        [Display(Name = "Текст")]
        public string Text { get; set; }
     
    }
}
