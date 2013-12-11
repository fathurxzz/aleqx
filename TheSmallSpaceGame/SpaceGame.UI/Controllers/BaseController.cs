using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SpaceGame.Api.Model.Entities;
using SpaceGame.DataAccess.Entities;
using SpaceGame.DataAccess.Repositories;
using SpaceGame.UI.Helpers;
using SpaceGame.UI.Models;

namespace SpaceGame.UI.Controllers
{
    public class BaseController : Controller
    {
        private readonly IPlanetRepository _repository;
        public List<PlanetPresentation> Planets { get; private set; }
        public IEnumerable<PlanetResource> PlanetResources { get; private set; }
        public IEnumerable<PlanetFacility> PlanetFacilities { get; private set; }
        protected short CurrentRoboticsLevel { get; private set; }
        protected short CurrentNaniteLevel { get; private set; }

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

                var resources = _repository.GetPlanetResources(WebSession.PlanetId);
                var facilities = _repository.GetPlanetFacilities(WebSession.PlanetId);
                var planetList = planets.Select(p => new PlanetPresentation { Id = p.Id, Name = p.Name }).ToList();
                PlanetResources = resources;
                PlanetFacilities = facilities;
                Planets = planetList;

                CurrentRoboticsLevel = PlanetFacilities.Single(f => f.FacilityId == (int)FacilityItem.RoboticsFactory).Level;
                CurrentNaniteLevel = PlanetFacilities.Single(f => f.FacilityId == (int)FacilityItem.NaniteFactory).Level;
            }

            base.Initialize(requestContext);
        }





    }
}
