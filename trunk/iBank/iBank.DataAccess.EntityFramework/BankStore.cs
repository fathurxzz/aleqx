using System.Data.Entity;
using iBank.DataAccess.Entities;
using iBank.DataAccess.EntityFramework.Mapping;

namespace iBank.DataAccess.EntityFramework
{
    public class BankStore:IBankStore
    {
        //readonly BankContext _context=new BankContext();

        public IDbSet<User> Users
        {
            //get { return _context.Users; }
            get; set;
        }

        public int SaveChanges()
        {
            //return _context.SaveChanges();
            return 1;
        }

    }


    //public partial class BankContext:DbContext
    //{
    //    static BankContext()
    //    {
    //        Database.SetInitializer<BankContext>(null);
    //    }

    //    public BankContext()
    //        : base("Name=connectionstring")
    //    {
    //    }

    //    public DbSet<User> Users { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Configurations.Add(new UserMap());
    //    }
    //}
}