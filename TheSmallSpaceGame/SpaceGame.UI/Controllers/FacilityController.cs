using System.Web.Mvc;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.DataAccess.Repositories;
using SpaceGame.UI.Helpers;

namespace SpaceGame.UI.Controllers
{
    [Authorize]
    public class FacilityController : Controller
    {
        private readonly IFacilityRepository _repository;

        public FacilityController(IFacilityRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Upgrade(int id)
        {
            try
            {
                _repository.UpdateFacility(id, WebSession.PlanetId);
            }
            catch (GameException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            
            return RedirectToAction("Facilities", "Home");
        }

    }
}
