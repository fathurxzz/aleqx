using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Listelli.App_LocalResources;

namespace Listelli.Models
{
    public class SubscribeFormModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "WrongEmail")]
        public string SubscribeEmail { get; set; }

        public string ErrorMessage { get; set; }
    }
}