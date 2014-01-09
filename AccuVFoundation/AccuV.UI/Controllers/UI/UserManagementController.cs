using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AccuV.UI.Security;
using AccuVAPI.Operations;

namespace AccuV.UI.Controllers.UI
{
    //[ClaimsAuthorize("Admin", AccessPermissions.Admin)]
    public class UserManagementController : Controller
    {
        private readonly IUserOperations _operations;
        private readonly IActivityOperations _activityOperations;

        public UserManagementController(IUserOperations operations, IActivityOperations activityOperations)
        {
            _operations = operations;
            _activityOperations = activityOperations;
        }

        //
        // GET: /UserManagement/
        public ActionResult Index()
        {
            return View();
        }
	}
}