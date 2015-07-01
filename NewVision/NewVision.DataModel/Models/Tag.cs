using System;
using System.Collections.Generic;

namespace NewVision.DataModel.Models
{
    public partial class Tag
    {
        public Tag()
        {
            this.AuthorCategories = new List<AuthorCategory>();
            this.Authors = new List<Author>();
            this.Categories = new List<Category>();
            this.Products = new List<Product>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string TitleUa { get; set; }
        public virtual ICollection<AuthorCategory> AuthorCategories { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
