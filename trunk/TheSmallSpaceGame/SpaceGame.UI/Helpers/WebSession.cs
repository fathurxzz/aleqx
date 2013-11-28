using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.UI.Helpers
{

    public static class WebSession
    {
        static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public static User User
        {
            get
            {
                if (Session["user"] != null)
                {
                    return (User) Session["user"];
                }
                //throw new UserException("Session has expired",UserError.Unknow);
                throw new HttpException(401, "Session has expired");
            }
            set
            {
                Session["user"] = value;
            }
        }
    }
}