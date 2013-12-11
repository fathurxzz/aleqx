using System;
using SpaceGame.Api.Clauses;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.Api.Extensions;
using SpaceGame.Api.Helpers;
using SpaceGame.Api.Model.Entities;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.Repositories;

namespace SpaceGame.Api.Repositories
{
    public class ResourceRepository:PlanetRepository, IResourceRepository
    {
        private readonly ISpaceStore _store;

        public ResourceRepository(ISpaceStore store) : base(store)
        {
            _store = store;
        }

        public void UpdateMetalMine(int planetId, short roboticsLevel, short naniteLevel)
        {

            var resources = GetPlanetResources(planetId);
            var resourceSet = ResourceHelper.GetResourceSet(resources);

            var needMetalAmountForUpgrade = UpgradeResourceCost.UpgradeMetalMineCost((short)(resourceSet.Metal.MineLevel + 1)).Metal;
            var needCrystalAmountForUpgrade = UpgradeResourceCost.UpgradeMetalMineCost((short)(resourceSet.Metal.MineLevel + 1)).Crystal;


            if (resourceSet.Metal.Amount < needMetalAmountForUpgrade || resourceSet.Crystal.Amount < needCrystalAmountForUpgrade)
            {
                throw new GameException("Not enough resources for upgrade metal mine", GameError.NotEnoughResources);
            }

            var upgradeTime = resourceSet.Metal.CalculateUpgradeTime(roboticsLevel, naniteLevel, GameEntity.MetalMine);
            var startDate = DateTime.Now;
            var finishDate = startDate.Add(upgradeTime);
            resourceSet.Metal.UpdateStart = startDate;
            resourceSet.Metal.UpdateFinish = finishDate;
            resourceSet.Metal.IsUpdating = true;
            resourceSet.Metal.Amount -= needMetalAmountForUpgrade;
            resourceSet.Crystal.Amount -= needCrystalAmountForUpgrade;

            _store.SaveChanges();
            
        }

        public void UpdateCrystalMine(int planetId, short roboticsLevel, short naniteLevel)
        {
            var resources = GetPlanetResources(planetId);
            var resourceSet = ResourceHelper.GetResourceSet(resources);

            var needMetalAmountForUpgrade = UpgradeResourceCost.UpgradeCrystalMineCost((short)(resourceSet.Crystal.MineLevel + 1)).Metal;
            var needCrystalAmountForUpgrade = UpgradeResourceCost.UpgradeCrystalMineCost((short)(resourceSet.Crystal.MineLevel + 1)).Crystal;

            if (resourceSet.Metal.Amount < needMetalAmountForUpgrade || resourceSet.Crystal.Amount < needCrystalAmountForUpgrade)
            {
                throw new GameException("Not enough resources for upgrade metal mine", GameError.NotEnoughResources);
            }

            var upgradeTime = resourceSet.Crystal.CalculateUpgradeTime(roboticsLevel, naniteLevel, GameEntity.CrystalMine);
            var startDate = DateTime.Now;
            var finishDate = startDate.Add(upgradeTime);
            resourceSet.Crystal.UpdateStart = startDate;
            resourceSet.Crystal.UpdateFinish = finishDate;
            resourceSet.Crystal.IsUpdating = true;
            resourceSet.Metal.Amount -= needMetalAmountForUpgrade;
            resourceSet.Crystal.Amount -= needCrystalAmountForUpgrade;

            _store.SaveChanges();

            //if (resourceSet.Metal.Amount >= needMetalAmountForUpgrade && resourceSet.Crystal.Amount >= needCrystalAmountForUpgrade)
            //{
            //    resourceSet.Metal.Amount -= needMetalAmountForUpgrade;
            //    resourceSet.Crystal.Amount -= needCrystalAmountForUpgrade;
            //    resourceSet.Crystal.MineLevel++;
            //    _store.SaveChanges();
            //}
            //else
            //{
            //    throw new GameException("Not enough resources for upgrade crystal mine", GameError.NotEnoughResources);
            //}
        }

        public void UpdateDeiteriumGenerator(int planetId, short roboticsLevel, short naniteLevel)
        {
            var resources = GetPlanetResources(planetId);
            var resourceSet = ResourceHelper.GetResourceSet(resources);

            var needMetalForUpgradeAmout = UpgradeResourceCost.UpgradeDeiteriumGeneratorCost((short)(resourceSet.Deiterium.MineLevel + 1)).Metal;
            var needCrystalForUpgradeAmout = UpgradeResourceCost.UpgradeDeiteriumGeneratorCost((short)(resourceSet.Deiterium.MineLevel + 1)).Crystal;

            if (resourceSet.Metal.Amount < needMetalForUpgradeAmout || resourceSet.Crystal.Amount < needCrystalForUpgradeAmout)
            {
                throw new GameException("Not enough resources for upgrade metal mine", GameError.NotEnoughResources);
            }

            var upgradeTime = resourceSet.Deiterium.CalculateUpgradeTime(roboticsLevel, naniteLevel, GameEntity.CrystalMine);
            var startDate = DateTime.Now;
            var finishDate = startDate.Add(upgradeTime);
            resourceSet.Deiterium.UpdateStart = startDate;
            resourceSet.Deiterium.UpdateFinish = finishDate;
            resourceSet.Deiterium.IsUpdating = true;
            resourceSet.Metal.Amount -= needMetalForUpgradeAmout;
            resourceSet.Crystal.Amount -= needCrystalForUpgradeAmout;

            _store.SaveChanges();

            //if (resourceSet.Metal.Amount >= needMetalForUpgradeAmout && resourceSet.Crystal.Amount >= needCrystalForUpgradeAmout)
            //{
            //    resourceSet.Metal.Amount -= needMetalForUpgradeAmout;
            //    resourceSet.Crystal.Amount -= needCrystalForUpgradeAmout;
            //    resourceSet.Deiterium.MineLevel++;
            //    _store.SaveChanges();
            //}
            //else
            //{
            //    throw new GameException("Not enough resources for upgrade deiterium generator", GameError.NotEnoughResources);
            //}
        }
    }
}