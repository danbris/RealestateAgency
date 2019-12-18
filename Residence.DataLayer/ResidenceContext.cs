using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Residence.DataLayer
{
    public class ResidenceContext : DbContext
    {
        public ResidenceContext() : base("ResidenceContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ResidenceContext, Migrations.Configuration>());
        }

        public DbSet<Housing> Houses { get; set; }
        public DbSet<Comodity> Comodities { get; set; }
    }
}
