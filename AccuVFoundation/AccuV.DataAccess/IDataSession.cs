using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AccuV.DataAccess.Contracts;
using AccuV.DataAccess.Entities;
using Task = AccuV.DataAccess.Entities.Task;

namespace AccuV.DataAccess
{
    public interface IDataSession : IDisposable
    {
        DbSet<Manager> Managers { get; set; }
        DbSet<Market> Markets { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<RolloutTracking> RolloutTracking { get; set; }
        DbSet<Site> Sites { get; set; }
        DbSet<Task> Tasks { get; set; }
        DbSet<ProjectLastModified> ProjectsLastModified { get; set; }
        DbSet<TaskComment> TaskComments { get; set; }
        DbSet<PermissionsAlias> PermissionsAliases { get; set; }
        DbSet<GCType> GcTypes { get; set; }
        DbSet<GeneralContractor> GeneralContractors { get; set; }
        DbSet<GCSite> GcSites { get; set; }
        DbSet<DocumentEntry> DocumentEntries { get; set; }
        DbSet<UploadedDocument> UploadedDocuments { get; set; }
        DbSet<RequiredDocument> RequiredDocuments { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Activity> Activities { get; set; }
        DbSet<UserActivity> UserActivities { get; set; }
        DbSet<ActivityType> ActivityTypes { get; set; }
        DbSet<Module> Modules { get; set; }
        DbSet<SecurityView> SecurityViews { get; set; }
        Database Database { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<IDictionary<string, object>>> GetCloseoutPackageViewData(CloseoutpackageViewRequest request);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<List<UserProjectModuleClaim>> GetUserProjectModules(string userName);
    }
}