using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TheSmallSpaceGame.DataAccess.Entities;
using TheSmallSpaceGame.DataAccess.EntityFramework.Models.Mapping;


namespace TheSmallSpaceGame.DataAccess.EntityFramework.Models
{
    public class GameStore : IGameStore
    {
        readonly GameContext _context = new GameContext();

        public IDbSet<User> Users
        {
            get { return _context.Users; }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }


    public partial class GameContext : DbContext
    {
        static GameContext()
        {
            Database.SetInitializer<GameContext>(null);
        }

        public GameContext()
            : base("Name=gbua_sgameContext")
        {
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourcesLevel> ResourcesLevels { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ResourceMap());
            modelBuilder.Configurations.Add(new ResourcesLevelMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
