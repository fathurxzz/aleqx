//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iBank.TestData
{
    using System;
    using System.Collections.Generic;
    
    public partial class OtpSms
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Password { get; set; }
        public System.DateTime ExpireDate { get; set; }
        public int TokenId { get; set; }
    
        public virtual Token Token { get; set; }
    }
}