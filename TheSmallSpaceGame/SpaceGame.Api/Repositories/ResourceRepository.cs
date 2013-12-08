using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.Api.Helpers;
using SpaceGame.Api.Repositories;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.Repositories;

namespace SpaceGame.Api
{
    public class ResourceRepository:PlanetRepository, IResourceRepository
    {
        private readonly ISpaceStore _store;

        public ResourceRepository(ISpaceStore store) : base(store)
        {
            _store = store;
        }

        public void UpdateMetalMine(int planetId)
        {

            var resources = GetPlanetResources(planetId);
            var resourceSet = ResourceHelper.GetResourceSet(resources);

            var needMetalAmountForUpgrade = UpgradeResourceCost.UpgradeMetalMineCost((short)(resourceSet.Metal.MineLevel + 1)).Metal;
            var needCrystalAmountForUpgrade = UpgradeResourceCost.UpgradeMetalMineCost((short)(resourceSet.Metal.MineLevel + 1)).Crystal;

            if (resourceSet.Metal.Amount >= needMetalAmountForUpgrade && resourceSet.Crystal.Amount >= needCrystalAmountForUpgrade)
            {
                resourceSet.Metal.Amount -= needMetalAmountForUpgrade;
                resourceSet.Crystal.Amount -= needCrystalAmountForUpgrade;
                resourceSet.Metal.MineLevel++;
                _store.SaveChanges();
            }
            else
            {
                throw new GameException("Not enough resources for upgrade metal mine", GameError.NotEnoughResources);
            }
        }

        public void UpdateCrystalMine(int planetId)
        {
            var resources = GetPlanetResources(planetId);
            var resourceSet = ResourceHelper.GetResourceSet(resources);

            var needMetalAmountForUpgrade = UpgradeResourceCost.UpgradeCrystalMineCost((short)(resourceSet.Crystal.MineLevel + 1)).Metal;
            var needCrystalAmountForUpgrade = UpgradeResourceCost.UpgradeCrystalMineCost((short)(resourceSet.Crystal.MineLevel + 1)).Crystal;

            if (resourceSet.Metal.Amount >= needMetalAmountForUpgrade && resourceSet.Crystal.Amount >= needCrystalAmountForUpgrade)
            {
                resourceSet.Metal.Amount -= needMetalAmountForUpgrade;
                resourceSet.Crystal.Amount -= needCrystalAmountForUpgrade;
                resourceSet.Crystal.MineLevel++;
                _store.SaveChanges();
            }
            else
            {
                throw new GameException("Not enough resources for upgrade crystal mine", GameError.NotEnoughResources);
            }
        }

        public void UpdateDeiteriumGenerator(int planetId)
        {
            var resources = GetPlanetResources(planetId);
            var resourceSet = ResourceHelper.GetResourceSet(resources);

            var needMetalForUpgradeAmout = UpgradeResourceCost.UpgradeDeiteriumGeneratorCost((short)(resourceSet.Deiterium.MineLevel + 1)).Metal;
            var needCrystalForUpgradeAmout = UpgradeResourceCost.UpgradeDeiteriumGeneratorCost((short)(resourceSet.Deiterium.MineLevel + 1)).Crystal;

            if (resourceSet.Metal.Amount >= needMetalForUpgradeAmout && resourceSet.Crystal.Amount >= needCrystalForUpgradeAmout)
            {
                resourceSet.Metal.Amount -= needMetalForUpgradeAmout;
                resourceSet.Crystal.Amount -= needCrystalForUpgradeAmout;
                resourceSet.Deiterium.MineLevel++;
                _store.SaveChanges();
            }
            else
            {
                throw new GameException("Not enough resources for upgrade deiterium generator", GameError.NotEnoughResources);
            }
        }
    }
}