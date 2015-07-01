using System;
using System.Collections.Generic;

namespace NewVision.UI.Models
{
    public partial class AuthorCategory
    {
        public AuthorCategory()
        {
            this.Categories = new List<Category>();
            this.Tags = new List<Tag>();
        }

        public int Id { get; set; }
        public int SortOrder { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string TitleUa { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
