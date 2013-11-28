using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SpaceGame.Data2.Models.Mapping;

namespace SpaceGame.Data2.Models
{
    public partial class gbua_spaceContext : DbContext
    {
        static gbua_spaceContext()
        {
            Database.SetInitializer<gbua_spaceContext>(null);
        }

        public gbua_spaceContext()
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
