using System;
using System.Collections.Generic;

namespace NewVision.UI.Models
{
    public partial class Category
    {
        public Category()
        {
            this.AuthorCategories = new List<AuthorCategory>();
            this.Tags = new List<Tag>();
        }

        public int Id { get; set; }
        public int SortOrder { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string TitleUa { get; set; }
        public virtual ICollection<AuthorCategory> AuthorCategories { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
