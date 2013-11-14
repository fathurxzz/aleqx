using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using CashMachine.DataAccess.Entities;

namespace CashMachine.DataAccess
{
    public interface ICardStore
    {
        IDbSet<Card> Cards { get; }
        IDbSet<Operation> Operations { get; }
        int SaveChanges();
    }
}
