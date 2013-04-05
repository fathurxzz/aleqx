using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Filimonov.Models
{
    [MetadataType(typeof(ProductValidation))]
    public partial class Product
    {
       
    }

    public class ProductValidation
    {
        [Required(ErrorMessage = "Обязательно! (Сначала заполните разделы)")]
        [DisplayName("Раздел")]
        public string LayoutId { get; set; }
    }
}