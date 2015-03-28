using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionIntention.UI.Models
{

    [MetadataType(typeof(TagValidation))]
    partial class Tag
    {

    }
    public class TagValidation
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
    }
}