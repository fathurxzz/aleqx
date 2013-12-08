using System;
using System.Collections.Generic;
using System.Linq;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.Api.Helpers;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.Entities;
using SpaceGame.DataAccess.Helpers;
using SpaceGame.DataAccess.Repositories;

namespace SpaceGame.Api.Repositories
{
    public class FacilityRepository : PlanetRepository, IFacilityRepository
    {
        private readonly ISpaceStore _store;

        public FacilityRepository(ISpaceStore store)
            : base(store)
        {
            _store = store;
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

        protected List<PlanetFacility> GetCurrentFacilities(int planetId)
        {
            return _store.PlanetFacilities.Where(p => p.PlanetId == planetId).ToList();
        }


        public void UpdateFacility(int facilityId, int planetId)
        {
            try
            {
                var resources = GetPlanetResources(planetId);
                var resourceSet = ResourceHelper.GetResourceSet(resources);
                var facilities = GetCurrentFacilities(planetId);

                var facility = facilities.SingleOrDefault(f => f.FacilityId == facilityId);
                if (facility == null)
                {
                    throw new GameException(string.Format("Facility {0} doesnt exist", (FacilityItem)facilityId), GameError.FacilityDoesNotExistOrUpdating);
                }

                if (facility.IsUpdating)
                {
                    throw new GameException(string.Format("Facility {0} is updating", (FacilityItem)facilityId), GameError.FacilityDoesNotExistOrUpdating);
                }

                var needMetalAmountForUpgrade = UpgrageFacilityCost.Cost((short)(facility.Level + 1), facility.Facility.MetalCost);
                var needCrystalAmountForUpgrade = UpgrageFacilityCost.Cost((short)(facility.Level + 1), facility.Facility.CrystalCost);
                var needDeiteriumAmountForUpgrade = UpgrageFacilityCost.Cost((short)(facility.Level + 1), facility.Facility.DeiteriumCost);

                if (resourceSet.Metal.Amount < needMetalAmountForUpgrade ||
                    resourceSet.Crystal.Amount < needCrystalAmountForUpgrade ||
                    resourceSet.Deiterium.Amount < needDeiteriumAmountForUpgrade)
                {
                    throw new GameException(string.Format("Not enough resources for {0} upgrade ", (FacilityItem)facilityId), GameError.NotEnoughResources);
                }

                var roboticsFactoryLevel = facilities.Single(f => f.FacilityId == (int) FacilityItem.RoboticsFactory).Level;
                var naniteFactoryLevel = facilities.Single(f => f.FacilityId == (int) FacilityItem.NaniteFactory).Level;

                var upgradeTime = UpgradeTime.Caclulate(needMetalAmountForUpgrade, needCrystalAmountForUpgrade, roboticsFactoryLevel, naniteFactoryLevel, (short) (facility.Level + 1), (GameEntity)((FacilityItem) facility.Id));

                var startDate = DateTime.Now;
                var finishDate = startDate.Add(upgradeTime);

                facility.UpdateStart = startDate;
                facility.UpdateFinish = finishDate;
                facility.IsUpdating = true;

                resourceSet.Metal.Amount -= needMetalAmountForUpgrade;
                resourceSet.Crystal.Amount -= needCrystalAmountForUpgrade;
                resourceSet.Deiterium.Amount -= needDeiteriumAmountForUpgrade;

                _store.SaveChanges();
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


    }
}