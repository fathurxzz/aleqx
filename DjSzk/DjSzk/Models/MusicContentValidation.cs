using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DjSzk.Models
{
    [MetadataType(typeof(MusicContentValidation))]
    public partial class MusicContent
    {

    }

    [Bind(Exclude = "Id")]
    public class MusicContentValidation
    {
        [Required(ErrorMessage = "* Введите порядок отображения (целое число)")]
        [DisplayName("Порядок отбражения")]
        public string SortOrder { get; set; }

        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Файл")]
        public string FileSource { get; set; }
    }
}