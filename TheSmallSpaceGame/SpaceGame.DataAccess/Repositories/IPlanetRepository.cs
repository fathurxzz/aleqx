using System.Collections.Generic;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess.Repositories
{
    public interface IPlanetRepository:IRepository
    {
        //Planet GetPlanet(int id, int userId);
        IEnumerable<Planet> GetPlanets(int userId);
        //ResourceType GetResourceType(int resourceTypeId);
        ResourceSet GetPlanetResources(int planetId);
        void UpdateMetalMine(int planetId);
        void UpdateCrystalMine(int planetId);
        void UpdateDeiteriumGenerator(int planetId);
        //bool ValidatePlanet(int userId, int planetId);

        ResourceProduceLevelSet GetLevelMines(int planetId);

        //void RecalculateResources(int planetId);

    }
}