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
    public class HomeController : BaseController
    {
        private readonly IPlanetRepository _repository;
        private readonly IFacilityRepository _frepository;

        public HomeController(IPlanetRepository repository, IFacilityRepository frepository)
            : base(repository)
        {
            _repository = repository;
            _frepository = frepository;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }



        [HandleError(ExceptionType = typeof(UserException), View = "Error")]
        public ActionResult Overview()
        {
            ViewBag.CurrentMenuItem = "overview";


            //var planets = _repository.GetPlanets(WebSession.User.Id).ToList();
            //var currentPlanet = planets.FirstOrDefault(p => p.Id == planet) ?? planets.First();
            //var resourceSet = _repository.GetPlanetResourceAmounts(currentPlanet.Id);
            //var planetList = planets.Select(p => new PlanetPresentation { Id = p.Id, Name = p.Name }).ToList();
            //return View(new GameViewModel { CurrentResourceAmountSet = resourceSet, Planets = planetList });


            return View(new GameViewModel
                        {
                            CurrentResourceAmountSet = CurrentResourceAmountSet,
                            Planets = Planets,
                            ErrorMessage = (string)TempData["errorMessage"]
                        });
        }




        public ActionResult Resources()
        {
            ViewBag.CurrentMenuItem = "resources";

            //var planets = _repository.GetPlanets(WebSession.User.Id).ToList();
            //var currentPlanet = planets.FirstOrDefault(p => p.Id == planet) ?? planets.First();
            //var resourceSet = _repository.GetPlanetResourceAmounts(WebSession.PlanetId);
            //var planetList = planets.Select(p => new PlanetPresentation { Id = p.Id, Name = p.Name }).ToList();

            var mineslevelSet = _repository.GetLevelMines(WebSession.PlanetId);
            return View(new ResourceViewModel
                        {
                            CurrentResourceAmountSet = CurrentResourceAmountSet,
                            Planets = Planets,
                            CurrentResourceProduceLevelSet = mineslevelSet,
                            //CurrentPlanetId = WebSession.PlanetId,
                            ErrorMessage = (string)TempData["errorMessage"] //GameErrorCodes.GetErrorMessage(errorCode)
                        });
        }

        public ActionResult Facilities()
        {
            ViewBag.CurrentMenuItem = "facilities";
            //var planets = _repository.GetPlanets(WebSession.User.Id).ToList();
            //var currentPlanet = planets.FirstOrDefault(p => p.Id == WebSession.PlanetId) ?? planets.First();
            //var resourceSet = _repository.GetPlanetResourceAmounts(currentPlanet.Id);
            //var planetList = planets.Select(p => new PlanetPresentation { Id = p.Id, Name = p.Name }).ToList();
            var facilities = _frepository.GetPlanetFacilities(WebSession.PlanetId).ToList();
            return View(new FacilityViewModel
                        {
                            CurrentResourceAmountSet = CurrentResourceAmountSet,
                            Planets = Planets,
                            PlanetFacilities = facilities,
                            ErrorMessage = (string)TempData["errorMessage"]
                        });
        }

    }
}
