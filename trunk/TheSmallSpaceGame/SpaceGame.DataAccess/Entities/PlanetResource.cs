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
    }

    public partial class PlanetResource
    {
        public ResourceValuesSet ResourceCostMetal
        {
            get
            {
                switch ((ResourceItem)ResourceId)
                {
                    case ResourceItem.Metal:
                        return UpgradeResourceCost.UpgradeMetalMineCost(MineLevel++);
                    case ResourceItem.Crystal:
                        return UpgradeResourceCost.UpgradeCrystalMineCost(MineLevel++);
                    case ResourceItem.Deiterium:
                        return UpgradeResourceCost.UpgradeDeiteriumGeneratorCost(MineLevel++);
                }

                throw new InvalidEnumArgumentException();
            }
        }
    }
}