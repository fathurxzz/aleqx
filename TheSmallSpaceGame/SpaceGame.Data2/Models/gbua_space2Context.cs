using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SpaceGame.Data2.Models.Mapping;

namespace SpaceGame.Data2.Models
{
    public partial class gbua_space2Context : DbContext
    {
        static gbua_space2Context()
        {
            Database.SetInitializer<gbua_space2Context>(null);
        }

        public gbua_space2Context()
            : base("Name=gbua_space2Context")
        {
        }

        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<PlanetFacility> PlanetFacilities { get; set; }
        public DbSet<PlanetResource> PlanetResources { get; set; }
        public DbSet<PlanetShip> PlanetShips { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FacilityMap());
            modelBuilder.Configurations.Add(new PlanetMap());
            modelBuilder.Configurations.Add(new PlanetFacilityMap());
            modelBuilder.Configurations.Add(new PlanetResourceMap());
            modelBuilder.Configurations.Add(new PlanetShipMap());
            modelBuilder.Configurations.Add(new ResourceMap());
            modelBuilder.Configurations.Add(new ShipMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
