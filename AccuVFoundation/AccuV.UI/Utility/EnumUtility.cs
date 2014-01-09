using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using AccuV.UI.Security;

namespace AccuV.UI.Utility
{
    public static class EnumUtility
    {
        public static bool Test(this AccessPermissions enumeration, AccessPermissions value)
        {
            return (enumeration & value) == value;
        }

        public static AccessPermissions ToAccessPermissions(this string value)
        {
            AccessPermissions result;
            var success = Enum.TryParse(value, out result);
            if (!success)
                throw new ArgumentException("Value cannot be converted to  AccessPermissions");
            return result;
        }
    }
}