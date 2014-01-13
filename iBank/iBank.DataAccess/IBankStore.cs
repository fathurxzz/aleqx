using System.Data.Entity;
using iBank.DataAccess.Entities;

namespace iBank.DataAccess
{
    public interface IBankStore
    {
        IDbSet<User> Users { get; }

        int SaveChanges();
    }
}