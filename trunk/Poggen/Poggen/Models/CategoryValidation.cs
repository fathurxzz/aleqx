using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poggen.Models
{
    [MetadataType(typeof(CategoryValidation))]
    public partial class Category
    {

    }

    public class CategoryValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Уникальнй идентификатор (выводится в строке адреса)")]
        [RegularExpression(@"^([a-zA-Z0-9]+)", ErrorMessage = "Неверный формат! Вводите латинские символы без пробелов")]
        
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Описание (для поисковиков)")]
        public string SeoDescription { get; set; }


        [DisplayName("Ключевые слова (для поисковиков)")]
        public string SeoKeywords { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }
    }
}