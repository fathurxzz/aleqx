using System.Data.Entity;
using NexiusFusion.DataAccess.Entities.Mapping;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class NexiusFusionContext : DbContext
    {
        static NexiusFusionContext()
        {
            Database.SetInitializer<NexiusFusionContext>(null);
        }

        public NexiusFusionContext()
            : base("Name=Rollout_TrackingContext")
        {
        }

        public DbSet<ReportEmployee> ReportEmployees { get; set; }
        public DbSet<ReportHazard> ReportHazards { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Hazard> Hazards { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<StructureType> StructureTypes { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Site> Sites { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ReportEmployeeMap());
            modelBuilder.Configurations.Add(new ReportHazardMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new EmployeeTypeMap());
            modelBuilder.Configurations.Add(new HazardMap());
            modelBuilder.Configurations.Add(new ReportMap());
            modelBuilder.Configurations.Add(new StructureTypeMap());
            modelBuilder.Configurations.Add(new WeatherMap());
            modelBuilder.Configurations.Add(new WorkTypeMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new SiteMap());
            modelBuilder.Configurations.Add(new PerDiemMap());
        }

        public System.Data.Entity.DbSet<NexiusFusion.DataAccess.Entities.PerDiem> PerDiems { get; set; }
    }
}
