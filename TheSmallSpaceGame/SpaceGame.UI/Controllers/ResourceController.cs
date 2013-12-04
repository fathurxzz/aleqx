using System.Web.Mvc;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.DataAccess.Repositories;
using SpaceGame.UI.Helpers;

namespace SpaceGame.UI.Controllers
{
    [Authorize]
    public class ResourceController : Controller
    {
        private readonly IResourceRepository _repository;

        public ResourceController(IResourceRepository repository)
        {
            _repository = repository;
        }

        public ActionResult UpgradeMetalMine()
        {
            try
            {
                _repository.UpdateMetalMine(WebSession.PlanetId);
            }
            catch (GameException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return RedirectToAction("Resources", "Home");
        }

        public ActionResult UpgradeCrystalMine()
        {
            try
            {
                _repository.UpdateCrystalMine(WebSession.PlanetId);
            }
            catch (GameException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return RedirectToAction("Resources", "Home");
        }

        public ActionResult UpgradeDeiteriumGenerator()
        {
            try
            {
                _repository.UpdateDeiteriumGenerator(WebSession.PlanetId);
            }
            catch (GameException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return RedirectToAction("Resources", "Home");
        }
    }
}
