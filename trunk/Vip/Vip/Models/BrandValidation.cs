using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vip.Models
{
    [MetadataType(typeof(BrandValidation))]
    public partial class Brand
    {
         
    }

    public class BrandValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }
    }
}