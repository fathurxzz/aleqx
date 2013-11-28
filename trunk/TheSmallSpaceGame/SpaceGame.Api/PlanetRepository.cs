using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.Entities;
using SpaceGame.DataAccess.Repositories;

namespace SpaceGame.Api
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly ISpaceStore _store;

        public PlanetRepository(ISpaceStore store)
        {
            _store = store;
        }


        //public ResourceSet GetResourceSet(Planet planet)
        //{

        //}




        //public Planet GetPlanet(int id, int userId)
        //{
        //    try
        //    {
        //        var planet = _store.Planets.FirstOrDefault(p => p.Id == id && p.User.Id == userId);
        //        if (planet == null)
        //        {
        //            throw new GameException(string.Format("Planet {0} for user {1} not found", id, userId), GameError.PlanetNotFound);
        //        }

        //        return planet;

        //    }
        //    catch (GameException)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new GameException("Repository is invalid: " + ex.Message, GameError.Unknow);
        //    }

        //}

        public IEnumerable<Planet> GetPlanets(int userId)
        {
            var planets = _store.Planets.Where(p => p.UserId == userId);
            return planets;
        }


        //public ResourceType GetResourceType(int resourceTypeId)
        //{
        //    return _store.ResourceTypes.First(r => r.Id == resourceTypeId);
        //}

        public ResourceSet GetPlanetResources(int planetId)
        {
            var resources = _store.Resources.Where(p => p.PlanetId == planetId);

            RecalculateResources(planetId);

            return new ResourceSet
                         {
                             Metal = (long)resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Metal).Amount,
                             Crystal = (long)resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Crystal).Amount,
                             Deiterium = (long)resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Deiterium).Amount
                         };
        }



        public bool ValidatePlanet(int userId, int planetId)
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

        public ResourceProduceLevelSet GetLevelMines(int planetId)
        {
            var resources = _store.Resources.Where(p => p.PlanetId == planetId);

            return new ResourceProduceLevelSet
                   {
                       MetalMine = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Metal).MineLevel,
                       CrystalMine = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Crystal).MineLevel,
                       DeiteriumGenerator = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Deiterium).MineLevel
                   };
        }

        public void UpdateMetalMine(int planetId)
        {
            var resources = _store.Resources.Where(p => p.PlanetId == planetId);
            RecalculateResources(planetId);
            var metal = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Metal);
            var crystal = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Crystal);

            var needMetalForUpgradeAmout = ResourceProduceLevelSet.UpgradeMetalMineCost((short)(metal.MineLevel + 1)).Metal;
            var needCrystalForUpgradeAmout = ResourceProduceLevelSet.UpgradeMetalMineCost((short)(metal.MineLevel + 1)).Crystal;

            // если хватает средств
            if (metal.Amount >= needMetalForUpgradeAmout && crystal.Amount >= needCrystalForUpgradeAmout)
            {
                metal.Amount -= needMetalForUpgradeAmout;
                crystal.Amount -= needCrystalForUpgradeAmout;
                metal.MineLevel++;
                _store.SaveChanges();
            }
            else
            {
                throw new GameException("Not enough resources for upgrade metal mine", GameError.NotEnoughResources);
            }
        }

        public void UpdateCrystalMine(int planetId)
        {
            var resources = _store.Resources.Where(p => p.PlanetId == planetId);
            RecalculateResources(planetId);
            var metal = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Metal);
            var crystal = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Crystal);

            var needMetalForUpgradeAmout = ResourceProduceLevelSet.UpgradeCrystalMineCost((short)(crystal.MineLevel + 1)).Metal;
            var needCrystalForUpgradeAmout = ResourceProduceLevelSet.UpgradeCrystalMineCost((short)(crystal.MineLevel + 1)).Crystal;

            // если хватает средств
            if (metal.Amount >= needMetalForUpgradeAmout && crystal.Amount >= needCrystalForUpgradeAmout)
            {
                metal.Amount -= needMetalForUpgradeAmout;
                crystal.Amount -= needCrystalForUpgradeAmout;
                crystal.MineLevel++;
                _store.SaveChanges();
            }
            else
            {
                throw new GameException("Not enough resources for upgrade crystal mine", GameError.NotEnoughResources);
            }
        }

        public void UpdateDeiteriumGenerator(int planetId)
        {
            var resources = _store.Resources.Where(p => p.PlanetId == planetId);
            RecalculateResources(planetId);
            var metal = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Metal);
            var crystal = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Crystal);
            var deiterium = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Deiterium);

            var needMetalForUpgradeAmout = ResourceProduceLevelSet.UpgradeDeiteriumGeneratorCost((short)(deiterium.MineLevel + 1)).Metal;
            var needCrystalForUpgradeAmout = ResourceProduceLevelSet.UpgradeDeiteriumGeneratorCost((short)(deiterium.MineLevel + 1)).Crystal;

            // если хватает средств
            if (metal.Amount >= needMetalForUpgradeAmout && crystal.Amount >= needCrystalForUpgradeAmout)
            {
                metal.Amount -= needMetalForUpgradeAmout;
                crystal.Amount -= needCrystalForUpgradeAmout;
                deiterium.MineLevel++;
                _store.SaveChanges();
            }
            else
            {
                throw new GameException("Not enough resources for upgrade deiterium generator", GameError.NotEnoughResources);
            }
        }

        private void RecalculateResources(int planetId)
        {
            var currDate = DateTime.Now;
            var resources = _store.Resources.Where(p => p.PlanetId == planetId);
            var metalResource = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Metal);
            var crystalResource = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Crystal);
            var deiteriumResource = resources.Single(r => r.ResourceTypeId == (int)ResourceItem.Deiterium);

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