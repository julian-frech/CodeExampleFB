using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DataOperator.Models;
using Microsoft.EntityFrameworkCore;

namespace DataOperator.Context
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options) { }

        public DbSet<ReportConfiguration> ReportConfigurations { get; set; }

        //public DbSet<Logs> logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
