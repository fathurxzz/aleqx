using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.DataAccess.Repositories;
using SpaceGame.UI.Helpers;

namespace SpaceGame.UI.Controllers
{
    [Authorize]
    public class ResourceController : Controller
    {
        private readonly IPlanetRepository _repository;

        public ResourceController(IPlanetRepository repository)
        {
            _repository = repository;
        }

        public ActionResult UpgradeMetalMine(int planet)
        {
            try
            {
                _repository.ValidatePlanet(WebSession.User.Id, planet);
                _repository.UpdateMetalMine(planet);
            }
            catch (GameException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Resources", "Home");
            }

            return RedirectToAction("Resources", "Home", new { planet = planet });
        }

        public ActionResult UpgradeCrystalMine(int planet)
        {
            try
            {
                _repository.ValidatePlanet(WebSession.User.Id, planet);
                _repository.UpdateCrystalMine(planet);
            }
            catch (GameException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Resources", "Home");
            }

            return RedirectToAction("Resources", "Home", new { planet = planet });
        }

        public ActionResult UpgradeDeiteriumGenerator(int planet)
        {
            try
            {
                _repository.ValidatePlanet(WebSession.User.Id, planet);
                _repository.UpdateDeiteriumGenerator(planet);
            }
            catch (GameException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Resources", "Home");
            }

            return RedirectToAction("Resources", "Home", new { planet = planet });
        }
    }
}
