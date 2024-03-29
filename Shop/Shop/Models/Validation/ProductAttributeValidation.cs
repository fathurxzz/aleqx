﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    [MetadataType(typeof(ProductAttributeValidation))]
    public partial class ProductAttribute
    {
         
    }

    public class ProductAttributeValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Тип значения")]
        public string ValueType { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [DisplayName("Отображать в кратком описании")]
        public bool ShowInCommonView { get; set; }

        [DisplayName("Статический")]
        public bool Static { get; set; }
    }
}