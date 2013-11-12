using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace CashMachine.Models
{
    public class SiteContext:DbContext
    {
        public SiteContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        DbSet<Card> Cards { get; set; }
        DbSet<Operation> Operations { get; set; }

        DbSet<OperationType> OperationTypes { get; set; }
    }
}