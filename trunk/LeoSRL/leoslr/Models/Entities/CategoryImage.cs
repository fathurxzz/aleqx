using System;
using System.Collections.Generic;

namespace Leo.Models
{
    public partial class CategoryImage
    {
        public int Id { get; set; }
        public string ImageSource { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
