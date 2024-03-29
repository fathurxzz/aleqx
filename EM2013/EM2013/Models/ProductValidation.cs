﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EM2013.Models
{
    [MetadataType(typeof(ProductValidation))]
    public partial class Product
    {

    }

    public class ProductValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Уникальнй идентификатор (выводится в строке адреса)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Превьюшка")]
        public string ImageSource { get; set; }

        [DisplayName("Описание (для поисковиков)")]
        public string SeoDescription { get; set; }


        [DisplayName("Ключевые слова (для поисковиков)")]
        public string SeoKeywords { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Дата")]
        public string Date { get; set; }
    }
}