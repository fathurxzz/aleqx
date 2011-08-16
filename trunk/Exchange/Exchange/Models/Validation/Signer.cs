using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    [MetadataType(typeof(SignerValidation))]
    public partial class Signer
    {

    }

    public class SignerValidation
    {
        [Required(ErrorMessage = "Заполнять обязательно!")]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Заполнять обязательно!")]
        [DisplayName("Должность")]
        public string Post { get; set; }
    }
}