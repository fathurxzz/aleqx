using System.Web;
using System.Web.SessionState;
using HavilaTravel.Models;

namespace HavilaTravel.Helpers
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

        public static PlaceKind PlaceKind
        {
            get
            {
                if (Session["placeKind"] == null)
                    Session["placeKind"] = PlaceKind.Region;
                return (PlaceKind)Session["placeKind"];
            }
            set { Session["placeKind"] = value; }
        }

        public static CurrentMenuHighlight CurrentMenuHighlight
        {
            get
            {
                if (Session["CurrentMenuHighlight"] == null)
                    Session["CurrentMenuHighlight"] = CurrentMenuHighlight.None;
                return (CurrentMenuHighlight)Session["CurrentMenuHighlight"];
            }
            set { Session["CurrentMenuHighlight"] = value; }
        }
    }

}