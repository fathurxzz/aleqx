using System.Data.Entity;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess
{
    public interface ISpaceStore
    {
        IDbSet<User> Users { get; }
        IDbSet<Planet> Planets { get; }
        IDbSet<PlanetResource> PlanetResources { get; }
        IDbSet<PlanetFacility> PlanetFacilities { get; }
        IDbSet<Facility> Facilities { get; }
        IDbSet<Resource> Resources { get; }
        int SaveChanges();
    }
}