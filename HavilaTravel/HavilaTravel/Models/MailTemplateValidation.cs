using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HavilaTravel.Models
{
    [MetadataType(typeof(MailTemplateValidation))]
    public partial class MailTemplate
    {
         
    }

    [Bind(Exclude = "Id")]
    public class MailTemplateValidation
    {
        [DisplayName("Название")]
        [Required(ErrorMessage = "* Введите название")]
        public string Title { get; set; }

    }
}