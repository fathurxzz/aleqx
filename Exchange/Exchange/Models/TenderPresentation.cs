using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class TenderPresentation:Tender
    {
        public string UserName { get; set; }
        public string UserPhone{ get; set; }


        
        public TenderPresentation(IDataRecord r)
            : base(r)
        {
            this.UserName = "aaa";
            this.UserPhone = "bbb";
        }


        public static TenderPresentation Init(TenderPresentation t, IDataRecord r)
        {
            t.UserName = r.GetValue<string>("user_name");
            t.UserPhone = r.GetValue<string>("user_phone");
            t.OfficeName = r.GetValue<string>("office_name");
            t.ParentOfficeName = r.GetValue<string>("parent_office_name");
            t.Podrid = r.GetValue<string>("podrid");
            return t;
        }
    }
}