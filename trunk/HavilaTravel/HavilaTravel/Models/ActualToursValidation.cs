using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HavilaTravel.Models
{
    [MetadataType(typeof(ActualToursValidation))]
    public partial class ActualTours
    {

    }

    [Bind(Exclude = "Id")]
    public class ActualToursValidation
    {
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Краткое описание")]
        public string Description { get; set; }

        [DisplayName("Цена")]
        [Required(ErrorMessage = "* обязательное поле (число)")]
        public string Price { get; set; }

        [DisplayName("Надпись 1")]
        public string Sign1 { get; set; }

        [DisplayName("Надпись 2")]
        public string Sign2 { get; set; }

        [DisplayName("Надпись 3")]
        public string Sign3 { get; set; }

        [DisplayName("Надпись 4")]
        public string Sign4 { get; set; }

        [DisplayName("Надпись 5")]
        public string Sign5 { get; set; }

    }
}