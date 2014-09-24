using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiki.DataAccess.Entities
{
    [MetadataType(typeof(ReasonValidation))]
    partial class Reason
    {

    }

    class ReasonValidation
    {
        [Display(Name = "Порядок отображения")]
        public int SortOrder { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Заголовок (ENG)")]
        public string TitleEng { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Текст (ENG)")]
        public string TextEng { get; set; }
    }
}
