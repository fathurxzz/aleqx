using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpaceGame.UI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("NickName:")]
        public string Name { get; set; }
    }
}