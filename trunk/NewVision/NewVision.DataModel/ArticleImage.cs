//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewVision.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ArticleImage
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public int ArticleId { get; set; }
    
        public virtual Article Article { get; set; }
    }
}
