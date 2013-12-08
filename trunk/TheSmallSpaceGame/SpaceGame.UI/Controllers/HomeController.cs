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
        //private readonly IFacilityRepository _frepository;

        public HomeController(IPlanetRepository repository, IFacilityRepository frepository)
            : base(repository)
        {
            //_frepository = frepository;
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
            return View(new GameViewModel
                        {
                            Planets = Planets,
                            PlanetResources = PlanetResources,
                            ErrorMessage = (string)TempData["errorMessage"]
                        });
        }

        public ActionResult Resources()
        {
            ViewBag.CurrentMenuItem = "resources";

            return View(new GameViewModel
                        {
                            Planets = Planets,
                            PlanetResources = PlanetResources,
                            PlanetFacilities = PlanetFacilities,
                            ErrorMessage = (string)TempData["errorMessage"]
                        });
        }

        public ActionResult Facilities()
        {
            ViewBag.CurrentMenuItem = "facilities";
            //var facilities = _frepository.GetPlanetFacilities(WebSession.PlanetId).ToList();

            return View(new FacilityViewModel
                        {
                            Planets = Planets,
                            PlanetFacilities = PlanetFacilities,
                            PlanetResources = PlanetResources,
                            ErrorMessage = (string)TempData["errorMessage"]
                        });
        }

    }
}
