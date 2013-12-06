using System;

namespace SpaceGame.DataAccess.Entities
{
    public partial class PlanetFacility
    {
        public int Id { get; set; }
        public short Level { get; set; }
        public DateTime UpdateStart { get; set; }
        public DateTime UpdateFinish { get; set; }
        public bool IsUpdating { get; set; }
        public int PlanetId { get; set; }
        public int FacilityId { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual Planet Planet { get; set; }
    }

    public partial class PlanetFacility
    {
        public TimeSpan CalculateUpgradeTime(short roboticsLevel, short naniteLevel)
        {
            return UpgradeTime.Caclulate(UpgradeFacilityCostMetal, UpgradeFacilityCostCrystal, roboticsLevel, naniteLevel, (short) (Level + 1));
        }

        public bool IsAvailableForUpgrade { get; set; }
        
        public long UpgradeFacilityCostMetal
        {
            get
            {
                return UpgrageFacilityCost.Cost((short)(Level + 1), Facility.MetalCost);
            }
        }

        public long UpgradeFacilityCostCrystal
        {
            get
            {
                return UpgrageFacilityCost.Cost((short)(Level + 1), Facility.CrystalCost);
            }
        }

        public long UpgradeFacilityCostDeiterium
        {
            get
            {
                return UpgrageFacilityCost.Cost((short)(Level + 1), Facility.DeiteriumCost);
            }
        }

        
    }
}