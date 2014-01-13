using iBank.DataAccess.Entities;

namespace iBank.DataAccess.Repositories
{
    public interface IUserRepository : IRepository
    {
        User GetUser(int id);
    }
}