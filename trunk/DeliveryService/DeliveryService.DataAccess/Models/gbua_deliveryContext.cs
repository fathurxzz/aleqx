using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DeliveryService.DataAccess.Models.Mapping;

namespace DeliveryService.DataAccess.Models
{
    public partial class gbua_deliveryContext : DbContext
    {
        static gbua_deliveryContext()
        {
            Database.SetInitializer<gbua_deliveryContext>(null);
        }

        public gbua_deliveryContext()
            : base("Name=gbua_deliveryContext")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyLocalInfo> CompanyLocalInfoes { get; set; }
        public DbSet<Criteria> Criteries { get; set; }
        public DbSet<MasterCategory> MasterCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLocalInfo> ProductLocalInfoes { get; set; }
        public DbSet<Quality> Qualities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CompanyLocalInfoMap());
            modelBuilder.Configurations.Add(new CriterionMap());
            modelBuilder.Configurations.Add(new MasterCategoryMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductLocalInfoMap());
            modelBuilder.Configurations.Add(new QualityMap());
        }
    }
}
