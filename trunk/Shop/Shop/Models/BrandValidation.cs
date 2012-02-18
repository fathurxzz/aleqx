using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    [MetadataType(typeof(BrandValidation))]
    public partial class Brand
    {
         
    }

    public class BrandValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Логотип")]
        public string Logo { get; set; }


        [DisplayName("Описание (для поисковиков)")]
        public string SeoDescription { get; set; }


        [DisplayName("Ключевые слова (для поисковиков)")]
        public string SeoKeywords { get; set; }

    }
}