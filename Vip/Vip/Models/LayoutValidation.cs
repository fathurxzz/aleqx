using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vip.Models
{
    [MetadataType(typeof(LayoutValidation))]
    public partial class Layout
    {

    }
    public class LayoutValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }
    }
}