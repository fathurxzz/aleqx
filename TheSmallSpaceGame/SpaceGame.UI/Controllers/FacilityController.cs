using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaceGame.DataAccess.Repositories;
using SpaceGame.UI.Helpers;

namespace SpaceGame.UI.Controllers
{
    public class FacilityController : Controller
    {

        private IFacilityRepository _repository;

        public FacilityController(IFacilityRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Upgrade(int id)
        {
            _repository.UpdateFacility(id, WebSession.PlanetId);
            return RedirectToAction("Facilities", "Home");
        }

    }
}
