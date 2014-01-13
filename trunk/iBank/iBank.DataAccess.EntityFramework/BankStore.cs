using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using iBank.DataAccess.Entities;
using iBank.DataAccess.EntityFramework.Mapping;

namespace iBank.DataAccess.EntityFramework
{
    public class BankStore:IBankStore
    {
        readonly BankContext _context=new BankContext();

        public IDbSet<User> Users
        {
            get { return _context.Users; }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

    }


    public partial class BankContext : DbContext
    {
        static BankContext()
        {
            Database.SetInitializer<BankContext>(null);
        }

        public BankContext()
            : base("Name=DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }

        public DbSet<OtpSms> OtpSmses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<TokenType> TokenTypes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}