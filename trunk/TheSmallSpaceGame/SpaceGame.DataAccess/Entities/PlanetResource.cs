using System;
using System.ComponentModel;

namespace SpaceGame.DataAccess.Entities
{
    public partial class PlanetResource
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public short MineLevel { get; set; }
        public int ResourceId { get; set; }
        public int PlanetId { get; set; }
        public virtual Planet Planet { get; set; }
        public virtual Resource Resource { get; set; }
        public DateTime UpdateStart { get; set; }
        public DateTime UpdateFinish { get; set; }
        public bool IsUpdating { get; set; }


        //public ResourceValuesSet UpdateToNextLevelCost
        //{
        //    get
        //    {
        //        switch ((ResourceItem)ResourceId)
        //        {
        //            case ResourceItem.Metal:
        //                return UpgradeResourceCost.UpgradeMetalMineCost((short)(MineLevel + 1));
        //            case ResourceItem.Crystal:
        //                return UpgradeResourceCost.UpgradeCrystalMineCost((short)(MineLevel + 1));
        //            case ResourceItem.Deiterium:
        //                return UpgradeResourceCost.UpgradeDeiteriumGeneratorCost((short)(MineLevel + 1));
        //        }

        //        throw new InvalidEnumArgumentException();
        //    }
        //}


        //public TimeSpan CalculateUpgradeTime(short roboticsLevel, short naniteLevel, GameEntity gameEntity)
        //{
        //    switch ((ResourceItem)ResourceId)
        //    {
        //        case ResourceItem.Metal:
        //            return UpgradeTime.Caclulate(UpdateToNextLevelCost.Metal, UpdateToNextLevelCost.Crystal, roboticsLevel, naniteLevel, (short)(MineLevel + 1), gameEntity);
        //        case ResourceItem.Crystal:
        //            return UpgradeTime.Caclulate(UpdateToNextLevelCost.Metal, UpdateToNextLevelCost.Crystal, roboticsLevel, naniteLevel, (short)(MineLevel + 1), gameEntity);
        //        case ResourceItem.Deiterium:
        //            return UpgradeTime.Caclulate(UpdateToNextLevelCost.Metal, UpdateToNextLevelCost.Crystal, roboticsLevel, naniteLevel, (short)(MineLevel + 1), gameEntity);
        //    }
        //    throw new InvalidEnumArgumentException();
        //}
    }

    
}