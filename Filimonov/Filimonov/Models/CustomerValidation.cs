using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Filimonov.Models
{
    [MetadataType(typeof(CustomerValidation))]
    public partial class Customer
    {

    }


    public class CustomerValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Имя")]
        public string Title { get; set; }
    }
}