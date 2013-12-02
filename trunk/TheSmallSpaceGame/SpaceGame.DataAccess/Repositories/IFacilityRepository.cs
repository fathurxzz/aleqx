using System.Collections.Generic;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess.Repositories
{
    public interface IFacilityRepository : IRepository
    {
        IEnumerable<PlanetFacility> GetPlanetFacilities(int planetId);
        IEnumerable<Facility> GetFacilities();
    }
}