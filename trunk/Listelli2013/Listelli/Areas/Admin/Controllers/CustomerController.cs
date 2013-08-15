using System.Web.Mvc;
using System.Web.Security;

namespace Listelli.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            return View(users);
        }
    }
}
