using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    [MetadataType(typeof(UserValidation))]
    public partial class User
    {
    }

    public class UserValidation
    {
        [Required(ErrorMessage = "Заполнять обязательно!")]
        [DisplayName("Телефон")]
        public string Phone { get; set; }
    }
}