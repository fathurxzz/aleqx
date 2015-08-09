using System;
using System.Collections.Generic;

namespace NewVision.DataModel.Models
{
    public partial class Author
    {
        public Author()
        {
            this.Products = new List<Product>();
            this.Events = new List<Event>();
            this.Tags = new List<Tag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Avatar { get; set; }
        public int SortOrder { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string TitleUa { get; set; }
        public string About { get; set; }
        public string AboutEn { get; set; }
        public string AboutUa { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionUa { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
