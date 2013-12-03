using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.Entities;
using SpaceGame.DataAccess.Repositories;

namespace SpaceGame.Api
{
    public class FacilityRepository : PlanetRepository, IFacilityRepository
    {
        private readonly ISpaceStore _store;

        public FacilityRepository(ISpaceStore store)
            : base(store)
        {
            _store = store;
        }

        public IEnumerable<PlanetFacility> GetPlanetFacilities(int planetId)
        {
            try
            {
                var facilities = _store.PlanetFacilities.Where(p => p.PlanetId == planetId);
                return facilities;
            }
            catch (Exception ex)
            {
                throw new GameException("Repository is invalid: " + ex.Message, GameError.Unknow);
            }
        }

        public IEnumerable<Facility> GetFacilities()
        {
            try
            {
                var facilities = _store.Facilities.ToList();
                return facilities;
            }
            catch (Exception ex)
            {
                throw new GameException("Repository is invalid: " + ex.Message, GameError.Unknow);
            }
        }

        public void UpdateFacility(int facilityId, int planetId)
        {
            var resources = _store.PlanetResources.Where(p => p.PlanetId == planetId);
            RecalculateResources(planetId);
            var metal = resources.Single(r => r.ResourceId == (int)ResourceItem.Metal);
            var crystal = resources.Single(r => r.ResourceId == (int)ResourceItem.Crystal);
        }


    }
}