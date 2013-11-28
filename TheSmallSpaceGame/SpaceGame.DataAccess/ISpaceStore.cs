using System.Data.Entity;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess
{
    public interface ISpaceStore
    {
        IDbSet<User> Users { get; }
        IDbSet<Planet> Planets { get; }
        IDbSet<ResourceType> ResourceTypes { get; }
        IDbSet<Resource> Resources { get; }
        int SaveChanges();
    }
}