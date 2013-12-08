using System;
using System.ComponentModel;
using SpaceGame.Api.Clauses;
using SpaceGame.Api.Model.Entities;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.Api.Extensions
{
    public static class PlanetResourceExtension
    {
        public static ResourceValuesSet UpdateToNextLevelCost(this PlanetResource source)
        {

            switch ((ResourceItem)source.ResourceId)
                {
                    case ResourceItem.Metal:
                        return UpgradeResourceCost.UpgradeMetalMineCost((short)(source.MineLevel + 1));
                    case ResourceItem.Crystal:
                        return UpgradeResourceCost.UpgradeCrystalMineCost((short)(source.MineLevel + 1));
                    case ResourceItem.Deiterium:
                        return UpgradeResourceCost.UpgradeDeiteriumGeneratorCost((short)(source.MineLevel + 1));
                }

                throw new InvalidEnumArgumentException();
            
        }


        public static TimeSpan CalculateUpgradeTime(this PlanetResource source, short roboticsLevel, short naniteLevel, GameEntity gameEntity)
        {
            switch ((ResourceItem)source.ResourceId)
            {
                case ResourceItem.Metal:
                    return UpgradeTime.Caclulate(source.UpdateToNextLevelCost().Metal, source.UpdateToNextLevelCost().Crystal, roboticsLevel, naniteLevel, (short)(source.MineLevel + 1), gameEntity);
                case ResourceItem.Crystal:
                    return UpgradeTime.Caclulate(source.UpdateToNextLevelCost().Metal, source.UpdateToNextLevelCost().Crystal, roboticsLevel, naniteLevel, (short)(source.MineLevel + 1), gameEntity);
                case ResourceItem.Deiterium:
                    return UpgradeTime.Caclulate(source.UpdateToNextLevelCost().Metal, source.UpdateToNextLevelCost().Crystal, roboticsLevel, naniteLevel, (short)(source.MineLevel + 1), gameEntity);
            }
            throw new InvalidEnumArgumentException();
        }
    }
}