using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    [MetadataType(typeof(CategoryValidation))]
    public partial class Category
    {

    }

    public class CategoryValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Веб-имя страницы (отображается в строке браузера)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [DisplayName("Описание")]
        public string SeoDescription { get; set; }

        [DisplayName("Ключевые слова")]
        public string SeoKeywords { get; set; }
    }
}