using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    [MetadataType(typeof(TagValidation))]
    public partial class Tag
    {
         
    }

    public class TagValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Название")]
        public string Value { get; set; }
    }
}