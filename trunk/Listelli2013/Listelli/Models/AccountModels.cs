using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using Listelli.App_LocalResources;

namespace Listelli.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Логин")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Обязательно!")]

        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "EnterPassword")]
        public string Password { get; set; }

        [DisplayName("Запомнить меня?")]
        public bool RememberMe { get; set; }
    }

    public class CustomerLogOnModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalRes), Name = "Password")]

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "EnterPassword")]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "UserNameRequired")]
        [Display(ResourceType = typeof(GlobalRes), Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "EmailRequired")]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(GlobalRes), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "PasswordRequired")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalRes), Name = "YourPassword")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }
    }
}
