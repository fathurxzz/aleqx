using System.Diagnostics;
using System.Web.Mvc;
using AccuV.DataAccess.Contracts;
using AccuV.UI.Models;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using AccuV.UI.Security;
using System.Security.Claims;
using Newtonsoft.Json;


namespace AccuV.UI.Controllers.UI
{
    public class HomeController : Controller
    {
        private List<string> _availableProjects = new List<string> { "NDPATT", "SRT" };
        private List<string> _availableModules = new List<string> { "Milestone Tracker", "Closeout Package" };
        private List<string> _adminModules = new List<string> { "User Management", "Activity Management" };

        //
        // GET: /Home/
        public ActionResult Index()
        {

            return View();
        }

        [AllowAnonymous]
        public ActionResult Test()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult TestResponse()
        {
            Response.AddHeader("Access-Control-Allow-Origin", "*");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var identity = User.Identity as ClaimsIdentity;
            var projectModules = identity.Claims
                .Where(c => c.Type == "Project")
                .Select(c => JsonConvert.DeserializeObject<UserProjectModuleClaim>(c.Value));


            if (identity.IsAdmin())
            {
                var menuModel =
                    _availableProjects.ToDictionary(p => p,
                            p => _availableModules.Select(m => new UserProjectModuleClaim {Module = m, ProjectId = p}));
                menuModel.Add("Admin",
                    _adminModules.Select(am => new UserProjectModuleClaim {ProjectId = "Admin", Module = am}));
                return PartialView(menuModel);
            }
            else
            {
                var menuModel = projectModules.Where(pm=>pm.ProjectId != null)
                    .GroupBy(pm => pm.ProjectId)
                    .ToDictionary(gr => gr.Key, gr => gr.AsEnumerable());
                return PartialView(menuModel);
            }

        }

    }
}