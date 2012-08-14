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
    }
}