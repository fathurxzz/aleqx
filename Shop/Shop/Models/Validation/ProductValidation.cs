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
        [DisplayName("Артикул")]
        public string Articul { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Уникальнй идентификатор (выводится в строке адреса)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Название")]
        public string Title { get; set; }

        [DisplayName("Описание (для поисковиков)")]
        public string SeoDescription { get; set; }


        [DisplayName("Ключевые слова (для поисковиков)")]
        public string SeoKeywords { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }


        [DisplayName("Краткое описание")]
        public string ShortDescription { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Новый")]
        public bool IsNew { get; set; }

        [DisplayName("Специальное предложение")]
        public bool IsSpecialOffer { get; set; }

        [DisplayName("Опубликовать")]
        public bool Published { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Старая цена")]
        public decimal OldPrice { get; set; }
    }
}