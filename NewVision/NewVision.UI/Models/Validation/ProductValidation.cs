using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewVision.UI.Models
{
    [MetadataType(typeof(ProductValidation))]
    partial class Product
    {

    }
    public class ProductValidation
    {
        [Display(Name = "Заголовок RU")]
        public string Title { get; set; }

        [Display(Name = "Заголовок EN")]
        public string TitleEn { get; set; }

        [Display(Name = "Заголовок UA")]
        public string TitleUa { get; set; }

        [Display(Name = "Мастер")]
        public string Author { get; set; }
        
        [Display(Name = "Цена")]
        public string Price { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSrc { get; set; }

        [Display(Name = "Тэги")]
        public string Tags { get; set; }

        [Display(Name = "Порядок отображения")]
        public int SortOrder { get; set; }
    }
}