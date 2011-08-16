using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class Moderator:UserPresentation
    {
        public int ModeratorOfficeCount { get; private set; }

        public static Moderator InitModerator(Moderator m, IDataRecord r)
        {
            m = (Moderator)InitUserPresentation(m, r);
            m.ModeratorOfficeCount = r.GetValue<int>("moderator_office_cnt");
            return m;
        }
    }
}