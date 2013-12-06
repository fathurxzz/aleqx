using System;

namespace SpaceGame.DataAccess
{
    public class UpgradeResourceCost
    {
        public static ResourceValuesSet UpgradeMetalMineCost(short level)
        {
            var cost = new ResourceValuesSet
            {
                Metal = (long)(60 * Math.Pow(1.5, level - 1)),
                Crystal = (long)(15 * Math.Pow(1.5, level - 1))
            };
            return cost;
        }

        public static ResourceValuesSet UpgradeCrystalMineCost(short level)
        {
            var cost = new ResourceValuesSet
            {
                Metal = (long)(48 * Math.Pow(1.6, level - 1)),
                Crystal = (long)(24 * Math.Pow(1.6, level - 1))
            };
            return cost;
        }

        public static ResourceValuesSet UpgradeDeiteriumGeneratorCost(short level)
        {
            var cost = new ResourceValuesSet
            {
                Metal = (long)(225 * Math.Pow(1.5, level - 1)),
                Crystal = (long)(75 * Math.Pow(1.5, level - 1))
            };
            return cost;
        }
    }
}