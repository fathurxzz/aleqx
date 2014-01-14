using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iBank.UI.Models
{
    public class OtpConfirmViewModel
    {
        [Required]
        [Display(Name = "Пароль из смс")]
        public string OneTimePassword { get; set; }
    }
}