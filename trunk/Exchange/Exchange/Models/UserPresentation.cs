using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class UserPresentation:User
    {
        public static UserPresentation InitUserPresentation(UserPresentation u, IDataRecord dr)
        {
            u.Id = dr.GetValue<int>("id");
            u.Name = dr.GetValue<string>("name").Trim();
            u.Phone = dr.GetValue("phone", string.Empty).Trim();
            u.TabNum = dr.GetValue<string>("tab_num").Trim();
            u.Podrid = dr.GetValue<string>("podrid").Trim();
            u.OfficeId = dr.GetValue<int>("office_id");
            u.ParentOfficeId = dr.GetValue<int>("parent_office_id");
            u.OfficeName = dr.GetValue<string>("office_name");
            u.ParentOfficeName = dr.GetValue<string>("parent_office_name");
            u.IsAutorized = dr.GetValue<bool>("is_autorized");
            return u;
        }
    }
}