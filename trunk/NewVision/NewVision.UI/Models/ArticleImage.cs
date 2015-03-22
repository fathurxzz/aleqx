using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewVision.UI.Models
{
    public partial class ArticleImage
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}