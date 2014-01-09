using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AccuV.DataAccess.Contracts;
using AccuV.UI.Utility;
using Newtonsoft.Json;

namespace AccuV.UI.Security
{
    public static class ClaimUtility
    {
        public static bool IsAdmin(this ClaimsIdentity identity)
        {
            var activities = identity.Claims.Where(c => c.Type == "Activity").Select(c=> JsonConvert.DeserializeObject<ActivityClaim>(c.Value));
            return activities.Any(a => a.Description == "Admin" && a.Permissions.Test(AccessPermissions.Admin));
        }
    }
}