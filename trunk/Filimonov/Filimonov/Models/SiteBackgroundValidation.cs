using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Filimonov.Models
{
   
    [MetadataType(typeof(SiteBackgroundValidation))]
    public partial class SiteBackground
    {

    }

    public class SiteBackgroundValidation
    {
        [DisplayName("Изображение")]
        public string ImageSource { get; set; }
    }
}