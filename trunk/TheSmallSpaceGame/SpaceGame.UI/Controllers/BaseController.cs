using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.Entities;
using SpaceGame.DataAccess.Repositories;
using SpaceGame.UI.Helpers;
using SpaceGame.UI.Models;

namespace SpaceGame.UI.Controllers
{
    public class BaseController : Controller
    {
        private readonly IPlanetRepository _repository;
        public ResourceAmountSet CurrentResourceAmountSet { get; private set; }
        public List<PlanetPresentation> Planets { get; private set; }

        public BaseController(IPlanetRepository repository)
        {
            _repository = repository;
        }


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (WebSession.User != null)
            {
                var planets = _repository.GetPlanets(WebSession.User.Id).ToList();
                var currentPlanet = planets.FirstOrDefault(p => p.Id == WebSession.PlanetId) ?? planets.First();
                WebSession.PlanetId = currentPlanet.Id;
                var resourceSet = _repository.GetPlanetResourceAmounts(currentPlanet.Id);
                var planetList = planets.Select(p => new PlanetPresentation {Id = p.Id, Name = p.Name}).ToList();
                CurrentResourceAmountSet = resourceSet;
                Planets = planetList;
            }

            base.Initialize(requestContext);
        }

    }
}
