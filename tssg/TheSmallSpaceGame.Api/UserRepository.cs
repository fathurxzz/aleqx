using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSmallSpaceGame.Api.Contracts;
using TheSmallSpaceGame.DataAccess;
using TheSmallSpaceGame.DataAccess.Entities;
using TheSmallSpaceGame.DataAccess.Repositories;
using Omu.ValueInjecter;

namespace TheSmallSpaceGame.Api
{
    public class UserRepository : IUserRepository
    {
        private IGameStore _store;

        public UserRepository(IGameStore store)
        {
            _store = store;
        }

        public User GetUser(string name)
        {
            try
            {
                var user = _store.Users.SingleOrDefault(u => u.Name == name);
                if (user == null)
                {
                    throw new UserException("User not found", UserError.NotFound);
                }
                return user;
            }
            catch (InvalidOperationException ex)
            {
                throw new UserException("Repository is invalid", UserError.Unknow);
            }

        }

        public void Save(User user)
        {
            var originalUser = _store.Users.SingleOrDefault(u => u.Id == user.Id);
            originalUser.InjectFrom(user);
            _store.SaveChanges();
        }
    }
}
