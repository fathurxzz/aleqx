using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mayka.Models
{
    [Table("Content")]
    public class Content
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Title { get; set; }
        public string MenuTitle { get; set; }
        public int SortOrder { get; set; }
        public string Text { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool MainPage { get; set; }
    }




}