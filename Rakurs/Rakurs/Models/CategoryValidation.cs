using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rakurs.Models
{
    [MetadataType(typeof(CategoryValidation))]
    public partial class Category
    {
         
    }

    public class CategoryValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Веб-имя страницы")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [DisplayName("Содержимое")]
        public string Text { get; set; }
    }
}