using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rakurs.Models
{
    [MetadataType(typeof(ProductValidation))]
    public partial class Product
    {

    }

    public class ProductValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        //[Required(ErrorMessage = "Обязательно!")]
        //[DisplayName("Порядок отображения")]
        //public int SortOrder { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Изображение")]
        [Required(ErrorMessage = "Обязательно!")]
        public string ImageSource { get; set; }

        [DisplayName("Отображать на главной странице")]
        public string ShowOnMainPage { get; set; }

        [DisplayName("Скидка")]
        public bool Discount { get; set; }

        [DisplayName("Текст скидки")]
        public string DiscountText { get; set; }

    }
}