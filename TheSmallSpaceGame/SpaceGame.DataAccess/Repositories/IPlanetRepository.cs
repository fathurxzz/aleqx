using System.Collections.Generic;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess.Repositories
{
    public interface IPlanetRepository:IRepository
    {
        //Planet GetPlanet(int id, int userId);
        IEnumerable<Planet> GetPlanets(int userId);
        //ResourceType GetResourceType(int resourceTypeId);
        
        //ResourceAmountSet GetPlanetResourceAmounts(IEnumerable<PlanetResource> resources );
        
        //bool ValidatePlanet(int userId, int planetId);

        //ResourceLevelSet GetLevelMines(IEnumerable<PlanetResource> resources);

        IEnumerable<PlanetResource> GetPlanetResources(int planetId);
        IEnumerable<PlanetFacility> GetPlanetFacilities(int planetId);
        //void RecalculateResources(int planetId);

    }
}