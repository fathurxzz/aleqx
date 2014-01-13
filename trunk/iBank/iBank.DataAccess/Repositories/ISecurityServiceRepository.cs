using iBank.DataAccess.Entities;

namespace iBank.DataAccess.Repositories
{
    public interface ISecurityServiceRepository:IRepository
    {
        User GetUser(int id);
        bool ValidateUser(string login, string password);
    }
}