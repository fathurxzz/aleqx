using System;
using SpaceGame.Api.Clauses;
using SpaceGame.Api.Model.Entities;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.Api.Extensions
{
    public static class PlanetFacilityExtension
    {
        public static TimeSpan CalculateUpgradeTime(this PlanetFacility source, short roboticsLevel, short naniteLevel)
        {
            return UpgradeTime.Caclulate(UpgradeFacilityCostMetal(source), UpgradeFacilityCostCrystal(source), roboticsLevel, naniteLevel, (short)(source.Level + 1), (GameEntity)((FacilityItem)source.FacilityId));
        }


        public static long UpgradeFacilityCostMetal(this PlanetFacility source)
        {
            return UpgrageFacilityCost.Cost((short)(source.Level + 1), source.Facility.MetalCost);
        }

        public static long UpgradeFacilityCostCrystal(this PlanetFacility source)
        {

            return UpgrageFacilityCost.Cost((short)(source.Level + 1), source.Facility.CrystalCost);
            
        }

        public static long UpgradeFacilityCostDeiterium(this PlanetFacility source)
        {

            return UpgrageFacilityCost.Cost((short)(source.Level + 1), source.Facility.DeiteriumCost);
            
        }
    }
}