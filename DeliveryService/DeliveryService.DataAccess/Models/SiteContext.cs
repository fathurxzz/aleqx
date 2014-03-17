using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DeliveryService.DataAccess.Models.Mapping;

namespace DeliveryService.DataAccess.Models
{
    public partial class SiteContext : DbContext
    {
        static SiteContext()
        {
            Database.SetInitializer<SiteContext>(null);
        }

        public SiteContext()
            : base("Name=gbua_deliveryContext")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyLocalInfo> CompanyLocalInfoes { get; set; }
        public DbSet<MasterCategory> MasterCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLocalInfo> ProductLocalInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CompanyLocalInfoMap());
            modelBuilder.Configurations.Add(new MasterCategoryMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductLocalInfoMap());
        }
    }
}
