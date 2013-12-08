using System.Collections.Generic;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess.Repositories
{
    public interface IFacilityRepository : IRepository
    {
        IEnumerable<Facility> GetFacilities();
        void UpdateFacility(int facilityId, int planetId);
    }
}