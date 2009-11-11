using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace CollectStickers.Models
{
    public static class MembershipExtentions
    {
        public static List<UserPresentation> GetAllUsers(this MembershipStorage context)
        {
            List<UserPresentation> result = new List<UserPresentation>();

            var users = (from user in context.aspnet_Users.Include("aspnet_Profile").Include("aspnet_Membership")
                         where user.UserName != "root" && user.UserName != "root2"
                         select new
                         {
                             userName = user.UserName,
                             isActive = user.aspnet_Membership.IsApproved,
                             email = user.aspnet_Membership.Email,
                             userId = user.UserId,
                             profileProperties = user.aspnet_Profile.PropertyNames,
                             profileValues = user.aspnet_Profile.PropertyValuesString
                         }).ToList();

            foreach (var item in users)
            {
                if (item.profileProperties != null)
                {
                    Dictionary<string, string> profileProperties = GetProfileProperties(item.profileProperties.Split(':'), item.profileValues);
                    UserPresentation user = GetUserPresentation(profileProperties);
                    if (user != null)
                    {
                        user.IsActive = item.isActive;
                        user.UserName = item.userName;
                        user.Email = item.email;
                        result.Add(user);
                    }
                }
            }
            return result;
        }

        private static Dictionary<string, string> GetProfileProperties(string[] names, string values)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (names != null && values != null)
            {
                try
                {
                    for (int i = 0; i < (names.Length / 4); i++)
                    {
                        string str = names[i * 4];
                        int startIndex = int.Parse(names[(i * 4) + 2], CultureInfo.InvariantCulture);
                        int length = int.Parse(names[(i * 4) + 3], CultureInfo.InvariantCulture);
                        if (((names[(i * 4) + 1] == "S") && (startIndex >= 0)) && ((length > 0) && (values.Length >= (startIndex + length))))
                        {
                            result[str] = values.Substring(startIndex, length);
                        }
                    }
                }
                catch
                {
                    result = null;
                }
            }
            return result;
        }

        private static UserPresentation GetUserPresentation(Dictionary<string, string> profileProperties)
        {
            UserPresentation result = null;
            if (profileProperties != null)
            {
                result = new UserPresentation();
                if (profileProperties.ContainsKey("Phone"))
                    result.Phone = profileProperties["Phone"];
                if (profileProperties.ContainsKey("Icq"))
                    result.Icq = profileProperties["Icq"];
                if (profileProperties.ContainsKey("UserId"))
                    result.Icq = profileProperties["UserId"];
            }
            return result;
        }

    }
}
