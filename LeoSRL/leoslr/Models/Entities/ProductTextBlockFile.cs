using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leo.Models
{
    public class ProductTextBlockFile
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public string FileName { get; set; }
        public int ProductTextBlockId { get; set; }
        public virtual ProductTextBlock ProductTextBlock { get; set; }
    }
}