using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    [MetadataType(typeof(DesignerContentValidation))]
    public partial class DesignerContent
    {

    }
    public class DesignerContentValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Название")]
        public string RoomTitle { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}