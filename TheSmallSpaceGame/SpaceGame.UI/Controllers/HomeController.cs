using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using SpaceGame.Api;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.Entities;
using SpaceGame.DataAccess.Repositories;
using SpaceGame.UI.Helpers;
using SpaceGame.UI.Models;

namespace SpaceGame.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPlanetRepository _repository;

        public HomeController(IPlanetRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }



        [HandleError(ExceptionType = typeof(UserException), View = "Error")]
        public ActionResult Overview(int? planet)
        {
            var planets = _repository.GetPlanets(WebSession.User.Id).ToList();
            var currentPlanet = planets.FirstOrDefault(p => p.Id == planet) ?? planets.First();
            var resourceSet = _repository.GetPlanetResources(currentPlanet.Id);
            var planetList = planets.Select(p => new PlanetPresentation { Id = p.Id, Name = p.Name }).ToList();
            ViewBag.CurrentMenuItem = "overview";
            return View(new GameViewModel { ResourceSet = resourceSet, Planets = planetList });
        }


        

        public ActionResult Resources(int? planet)
        {
            var planets = _repository.GetPlanets(WebSession.User.Id).ToList();
            var currentPlanet = planets.FirstOrDefault(p => p.Id == planet) ?? planets.First();
            var resourceSet = _repository.GetPlanetResources(currentPlanet.Id);
            var mineslevelSet = _repository.GetLevelMines(currentPlanet.Id);
            var planetList = planets.Select(p => new PlanetPresentation { Id = p.Id, Name = p.Name }).ToList();
            ViewBag.CurrentMenuItem = "resources";
            return View(new ResourceViewModel
                        {
                            ResourceSet = resourceSet, 
                            Planets = planetList, 
                            ResourceProduceLevelSet = mineslevelSet, 
                            CurrentPlanetId = currentPlanet.Id,
                            ErrorMessage = (string)TempData["errorMessage"] //GameErrorCodes.GetErrorMessage(errorCode)
                        });
        }

    }
}
