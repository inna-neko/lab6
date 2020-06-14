using Catalog.DAL.Entities;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.EF
{
    public class postContext
        : DbContext
    {
        public DbSet<pkge> pkges { get; set; }
        public DbSet<postoffice> postoffices { get; set; }
        public DbSet<worker> workers { get; set; }

        public postContext(DbContextOptions options)
            : base(options)
        {
        }

    }
}
