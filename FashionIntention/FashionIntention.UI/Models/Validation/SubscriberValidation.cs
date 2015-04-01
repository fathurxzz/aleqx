using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionIntention.UI.Models
{
    [MetadataType(typeof(SubscriberValidation))]
    partial class Subscriber
    {

    }

    public class SubscriberValidation
    {
        [Display(Name = "Email адрес")]
        public string Email { get; set; }

        [Display(Name = "Активный")]
        public bool IsActive { get; set; }
    }
}