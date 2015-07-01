using System;
using System.Collections.Generic;

namespace NewVision.UI.Models
{
    public partial class Product
    {
        public Product()
        {
            this.Tags = new List<Tag>();
        }

        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Price { get; set; }
        public int SortOrder { get; set; }
        public Nullable<System.DateTime> ViewDate { get; set; }
        public string ImageSrc { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string TitleUa { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
