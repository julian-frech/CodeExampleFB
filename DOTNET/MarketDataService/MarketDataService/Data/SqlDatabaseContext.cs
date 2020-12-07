using Microsoft.EntityFrameworkCore;
using MarketDataService.Models;
using System.Collections.Generic;


namespace MarketDataService.Data
{
    public class SqlDatabaseContext : DbContext
    {
        private readonly IEXCloudClassCompany iEXCloudClassCompany;

        public SqlDatabaseContext()
        {
        }

        public SqlDatabaseContext(DbContextOptions<SqlDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<SymbolClass> Symbols { get; set; }

        public DbSet<IEXCloudClassHistoric> IEXCloudClassHistorics { get; set; }

        public DbSet<IEXCloudClassSymbolList> IEXCloudClassSymbolLists { get; set; }

        public DbSet<IEXCloudClassCompany> IEXCloudClassCompanys { get; set; }

        public DbSet<IEXCloudClass> IEXCloudClasss { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IEXCloudClassCompany>().HasKey(c => new { c.symbol, c.companyName });
            modelBuilder.Entity<IEXCloudClassHistoric>().HasKey(c => new { c.symbol, c.date });
            modelBuilder.Entity<IEXCloudClassSymbolList>().HasKey(c => new { c.symbol, c.iexId });

            modelBuilder.Entity<IEXCloudClass>().HasKey(c => new { c.UniqueTableId });
            //modelBuilder.Entity<Symbols>().Property(a => a.Symbol).HasColumnType("varchar(48)");
            //modelBuilder.Entity<Symbols>().Property(a => a.ValidFrom).HasColumnType("DateTime");
            //modelBuilder.Entity<Symbols>().Property(a => a.ValidTo).HasColumnType("DateTime");
            //modelBuilder.Entity<Symbols>().Property(a => a.UTS).HasColumnType("DateTime");
        }

    }
}


