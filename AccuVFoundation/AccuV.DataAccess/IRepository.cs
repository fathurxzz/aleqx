using System.Data.Entity;

namespace AccuV.DataAccess
{
    public interface IRepository<T> : IDbSet<T> where T : class
    {

    }
}