//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class BlankItem
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string QuestionDescription { get; set; }
        public string Answer { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int BlankId { get; set; }
        public int SortOrder { get; set; }
    
        public virtual Blank Blank { get; set; }
    }
}