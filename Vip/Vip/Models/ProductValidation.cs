﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vip.Models
{
    [MetadataType(typeof(ProductValidation))]
    public partial class Product
    {
         
    }

    public class ProductValidation
    {
        

        [DisplayName("Изображение")]
        public int ImageSource { get; set; }
    }
}