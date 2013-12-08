using System;
using System.Collections.Generic;
using System.Linq;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.Api.Helpers;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.Entities;
using SpaceGame.DataAccess.Repositories;


namespace SpaceGame.Api.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly ISpaceStore _store;

        public PlanetRepository(ISpaceStore store)
        {
            _store = store;
        }

        public IEnumerable<Planet> GetPlanets(int userId)
        {
            var planets = _store.Planets.Where(p => p.UserId == userId);
            return planets;
        }

        public IEnumerable<PlanetFacility> GetPlanetFacilities(int planetId)
        {
            try
            {
                return _store.PlanetFacilities.Where(p => p.PlanetId == planetId).ToList();
            }
            catch (Exception ex)
            {
                throw new GameException("Repository is invalid: " + ex.Message, GameError.Unknow);
            }
        }

        public IEnumerable<PlanetResource> GetPlanetResources(int planetId)
        {
            try
            {
                CheckFacilityStatuses(planetId);
                return RecalculateResources(planetId);
            }
            catch (Exception ex)
            {
                throw new GameException("Repository is invalid: " + ex.Message, GameError.Unknow);
            }
        }

        public ResourceAmountSet GetPlanetResourceAmounts(IEnumerable<PlanetResource> resources)
        {
            var values = ResourceHelper.GetResourceSet(resources);
            return new ResourceAmountSet
            {
                Metal = (long)values.Metal.Amount,
                Crystal = (long)values.Crystal.Amount,
                Deiterium = (long)values.Deiterium.Amount
            };
        }


        protected bool ValidatePlanet(int userId, int planetId)
        {
            try
            {
                var planet = _store.Planets.FirstOrDefault(p => p.Id == planetId && p.User.Id == userId);
                if (planet == null)
                {
                    throw new GameException(string.Format("Planet {0} for user {1} not found", planetId, userId), GameError.PlanetNotFound);
                }
                return true;
            }
            catch (GameException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GameException("Repository is invalid: " + ex.Message, GameError.Unknow);
            }
        }


        private IEnumerable<PlanetResource> RecalculateResources(int planetId)
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

            return resources.ToList();
        }

        private void CheckFacilityStatuses(int planetId)
        {
            var facility = _store.PlanetFacilities.SingleOrDefault(f => f.PlanetId == planetId && f.IsUpdating);
            if (facility == null) return;
            if (facility.UpdateFinish > DateTime.Now) return;
            facility.Level++;
            facility.IsUpdating = false;
            _store.SaveChanges();
        }


        private double GetResourceDelta(ResourceItem resourceItem, int mineLevel, double totalMilliseconds)
        {
            int gameSpeed = 2;
            int coef = 0;
            switch (resourceItem)
            {
                case ResourceItem.Metal:
                    coef = 30;
                    break;
                case ResourceItem.Crystal:
                    coef = 20;
                    break;
                case ResourceItem.Deiterium:
                    coef = 10;
                    break;
            }


            var result = ((double)gameSpeed * (double)coef * (double)mineLevel * Math.Pow(1.1, mineLevel)) / 3600000 * totalMilliseconds;

            if (resourceItem == ResourceItem.Deiterium)
                return result;
            return result + (double)50 / (double)3600000 * totalMilliseconds;
        }
    }
}