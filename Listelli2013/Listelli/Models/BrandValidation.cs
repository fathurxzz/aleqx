using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    [MetadataType(typeof(BrandValidation))]
    public partial class Brand
    {

    }

    public class BrandValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Веб-имя страницы (отображается в строке браузера)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [DisplayName("Файл изображения")]
        public string ImageSource { get; set; }
    }
}