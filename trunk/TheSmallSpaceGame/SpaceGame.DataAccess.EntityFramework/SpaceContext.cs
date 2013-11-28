using System.Data.Entity;
using SpaceGame.DataAccess.Entities;
using SpaceGame.DataAccess.EntityFramework.Mapping;

namespace SpaceGame.DataAccess.EntityFramework
{
    public class SpaceStore : ISpaceStore
    {
        readonly SpaceContext _context = new SpaceContext();

        public IDbSet<User> Users
        {
            get { return _context.Users; }
        }

        public IDbSet<Planet> Planets
        {
            get { return _context.Planets; }
        }

        public IDbSet<ResourceType> ResourceTypes
        {
            get { return _context.ResourceTypes; }
        }

        public IDbSet<Resource> Resources
        {
            get { return _context.Resources; }
        }


        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }


    public partial class SpaceContext : DbContext
    {
        static SpaceContext()
        {
            Database.SetInitializer<SpaceContext>(null);
        }

        public SpaceContext()
            : base("Name=gbua_spaceContext")
        {
        }

        public DbSet<Planet> Planets { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PlanetMap());
            modelBuilder.Configurations.Add(new ResourceMap());
            modelBuilder.Configurations.Add(new ResourceTypeMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
