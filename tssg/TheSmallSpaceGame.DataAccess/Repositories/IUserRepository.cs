using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSmallSpaceGame.DataAccess.Entities;

namespace TheSmallSpaceGame.DataAccess.Repositories
{
    public interface IUserRepository:IRepository
    {
        User GetUser(string name);
        void Save(User user);
    }
}
