using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    [MetadataType(typeof(CategoryValidation))]
    partial class Category
    {

    }

    public class CategoryValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Порядок отображения")]
        [Required(ErrorMessage = "Введите порядок отображения")]
        public string SortOrder { get; set; }

        [Display(Name = "Идентификатор")]
        [Required(ErrorMessage = "Введите идентификатор")]
        public string Name { get; set; }
        
        [Display(Name = "Изображение")]
        public string ImageSource { get; set; }

        [Display(Name = "Доступность на текущем языке")]
        public bool IsCorrectLang { get; set; }

        [Display(Name = "Описание для поисковиков")]
        public string SeoDescription { get; set; }

        [Display(Name = "Ключевые слова для поисковиков")]
        public string SeoKeywords { get; set; }

        [Display(Name = "Текст для поисковиков")]
        public string SeoText { get; set; }
    }

}
