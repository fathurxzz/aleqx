using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess.Repositories
{
    public interface IUserRepository:IRepository
    {
        User GetUser(string email);
        User GetUser(int id);
        User Register(string email, string name);
    }
}