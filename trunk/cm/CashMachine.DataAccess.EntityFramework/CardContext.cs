using System.Collections.Generic;
using System.Data.Entity;
using CashMachine.DataAccess.EntityFramework.Mapping;

namespace CashMachine.DataAccess.Entities
{
    public class CardStore : ICardStore
    {
        readonly CardContext _context = new CardContext();

        public IDbSet<Card> Cards
        {
            get { return _context.Cards; }
        }

        public IDbSet<Operation> Operations
        {
            get { return _context.Operations; }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }

    public partial class CardContext : DbContext
    {
        static CardContext()
        {
            Database.SetInitializer<CardContext>(null);
        }

        public CardContext()
            : base("Name=gbua_cardContext")
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CardMap());
            modelBuilder.Configurations.Add(new OperationMap());
        }
    }
}
