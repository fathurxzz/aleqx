using iBank.DataAccess;
using iBank.DataAccess.Entities;
using iBank.DataAccess.Repositories;

namespace iBank.Api.Repositories
{
    public class SecurityServiceRepository:ISecurityServiceRepository
    {
        private readonly IBankStore _store;

        public SecurityServiceRepository(IBankStore store)
        {
            _store = store;   
        }

        public User GetUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool ValidateUser(string login, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}