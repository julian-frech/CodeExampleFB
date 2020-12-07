using Microsoft.EntityFrameworkCore;
using CalculatorService.Models;

namespace CalculatorService.Data
{
    public class SqlDatabaseContext : DbContext
    {
        
        public SqlDatabaseContext()
        {
        }

        public SqlDatabaseContext(DbContextOptions<SqlDatabaseContext> options)
            : base(options)
        {
            this.Database.SetCommandTimeout(180);
        }

        public DbSet<Analysis> Analyses { get; set; }

        public DbSet<CalculationData> CalculationDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Analysis>().HasKey(t => new { t.SymbolId, t.CalculationId, t.MarketTimestamp, t.Parameter });

            modelBuilder.Entity<CalculationData>().HasKey(k => new { k.symbol, k.MarketTimestamp, k.method });
        }

    }
}


