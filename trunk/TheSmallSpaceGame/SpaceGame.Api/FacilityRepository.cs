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
    public class FacilityRepository : IFacilityRepository
    {
        private readonly ISpaceStore _store;

        public FacilityRepository(ISpaceStore store)
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
        }

        private void RecalculateResources(int planetId)
        {
            var currDate = DateTime.Now;
            var resources = _store.PlanetResources.Where(p => p.PlanetId == planetId);
            var metalResource = resources.Single(r => r.ResourceId == (int)ResourceItem.Metal);
            var crystalResource = resources.Single(r => r.ResourceId == (int)ResourceItem.Crystal);
            var deiteriumResource = resources.Single(r => r.ResourceId == (int)ResourceItem.Deiterium);

            TimeSpan timeSpan = currDate - metalResource.LastUpdate;
            var deltaMetal = GetResourceDelta(ResourceItem.Metal, metalResource.MineLevel, timeSpan.TotalMilliseconds);
            var deltaCrystal = GetResourceDelta(ResourceItem.Crystal, crystalResource.MineLevel, timeSpan.TotalMilliseconds);
            var deltaDeiterium = GetResourceDelta(ResourceItem.Deiterium, deiteriumResource.MineLevel, timeSpan.TotalMilliseconds);

            metalResource.LastUpdate = currDate;
            crystalResource.LastUpdate = currDate;
            deiteriumResource.LastUpdate = currDate;

            metalResource.Amount += deltaMetal;
            crystalResource.Amount += deltaCrystal;
            deiteriumResource.Amount += deltaDeiterium;

            _store.SaveChanges();
        }
    }
}