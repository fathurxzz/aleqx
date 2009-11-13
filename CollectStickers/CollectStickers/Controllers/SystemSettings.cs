using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CollectStickers.Controllers
{
    public static class SystemSettings
    {
        public static Guid? CurrentUserId
        {
            get
            {
                MembershipUser user = Membership.GetUser();
                if (user != null)
                    return (Guid)user.ProviderUserKey;
                return null;
            }
        }

        public static int CurrentAlbumStickerCount
        {
            get { return 564; }
        }
    }
}
