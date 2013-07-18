using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    [MetadataType(typeof(ProductValidation))]
    public partial class Product
    {

    }

    public class ProductValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Веб-имя страницы (отображается в строке браузера)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Размер")]
        public string Size { get; set; }

        [DisplayName("Состав")]
        public string Composition { get; set; }

        [DisplayName("Производитель")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [DisplayName("Описание")]
        public string SeoDescription { get; set; }

        [DisplayName("Ключевые слова")]
        public string SeoKeywords { get; set; }

        [DisplayName("Файл изображения")]
        public string ImageSource { get; set; }
    }
}