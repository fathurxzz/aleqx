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

        public IDbSet<PlanetResource> PlanetResources
        {
            get { return _context.PlanetResources; }
        }

        public IDbSet<PlanetFacility> PlanetFacilities
        {
            get { return _context.PlanetFacilities; }
        }

        public IDbSet<Facility> Facilities
        {
            get { return _context.Facilities; }
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
