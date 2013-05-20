using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    [MetadataType(typeof(CategoryBrandValidation))]
    public partial class CategoryBrand
    {

    }

    public class CategoryBrandValidation
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

        
    }
}