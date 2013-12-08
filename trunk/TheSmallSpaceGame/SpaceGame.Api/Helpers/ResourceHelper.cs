using System.Collections.Generic;
using System.Linq;
using SpaceGame.Api.Model.Entities;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.Api.Helpers
{
    public class ResourceHelper
    {
        public static ResourceSet GetResourceSet(IEnumerable<PlanetResource> resources)
        {
            var metal = resources.Single(r => r.ResourceId == (int)ResourceItem.Metal);
            var crystal = resources.Single(r => r.ResourceId == (int)ResourceItem.Crystal);
            var deiterium = resources.Single(r => r.ResourceId == (int)ResourceItem.Deiterium);
            return new ResourceSet
            {
                Metal = metal,
                Crystal = crystal,
                Deiterium = deiterium
            };
        }

        //public static ResourceLevelSet GetResourceLevelSet(IEnumerable<PlanetResource> resources)
        //{
        //    var resourceSet = GetResourceSet(resources);
        //    return new ResourceLevelSet
        //    {
        //        MetalMine = resourceSet.Metal.MineLevel,
        //        CrystalMine = resourceSet.Crystal.MineLevel,
        //        DeiteriumGenerator = resourceSet.Deiterium.MineLevel
        //    };
        //}
    }
}
