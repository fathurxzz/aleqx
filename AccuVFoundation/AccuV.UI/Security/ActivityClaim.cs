using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AccuV.UI.Security
{
    public class ActivityClaim
    {
        public int ActivityId { get; set; }
        public AccessPermissions Permissions { get; set; }
        public string Description { get; set; }
        public int ActivityType { get; set; }

    }
}