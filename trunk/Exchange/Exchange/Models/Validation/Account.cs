using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    [MetadataType(typeof(AccountValidation))]
    public partial class Account
    {

    }

    public class AccountValidation
    {
        [Required(ErrorMessage = "Заполнять обязательно!")]
        [DisplayName("Счёт")]
        //[Display(Name = "labelForName", ResourceType = typeof(Resources.Resources))] 
        public string Nls { get; set; }
    }
}