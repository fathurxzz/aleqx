using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSmallSpaceGame.DataAccess.Entities;
using System.Data.Entity;

namespace TheSmallSpaceGame.DataAccess
{
    public interface IGameStore
    {
        IDbSet<User> Users { get; }
        int SaveChanges();
    }
}
