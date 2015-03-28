using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FashionIntention.UI.Models.Mapping;

namespace FashionIntention.UI.Models
{
    public partial class SiteContext : DbContext
    {
        static SiteContext()
        {
            Database.SetInitializer<SiteContext>(null);
        }

        public SiteContext()
            : base("Name=gbua_fashintContext")
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostItem> PostItems { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PostMap());
            modelBuilder.Configurations.Add(new PostItemMap());
            modelBuilder.Configurations.Add(new TagMap());
        }
    }
}
