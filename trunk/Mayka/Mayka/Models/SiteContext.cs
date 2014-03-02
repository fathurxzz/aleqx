using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mayka.Models
{
    public class SiteContext : DbContext
    {
        public SiteContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Content> Contents { get; set; }


    }
}