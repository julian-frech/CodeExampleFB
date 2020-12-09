using System;
using FiBroGraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace FiBroGraphQL.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {
        }

        public DbSet<MarketData> MarketDataList { get; set; }

        public DbSet<SymbolNews> SymbolNewsList { get; set; }

        public DbSet<SymbolFacts> SymbolFactsDataList { get; set; }

        public DbSet<LatestDate> latestDates { get; set; }

        public DbSet<UserAccount> userAccounts { get; set; }

        public DbSet<UserDepotMainObject> userDepotMainObj { get; set; }

        public DbSet<UserDepotF> userDepotFs { get; set; }

        public DbSet<UserDepotComponents> userDepotComponents { get; set; }

        public DbSet<UserDepotsView> userDepotsViews { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<SymbolObj>().HasKey(y => y.symbolId);

            modelBuilder.Entity<MarketData>().HasKey(y => new { y.symbol, y.MarketTimestamp });
            modelBuilder.Entity<SymbolNews>().HasKey(y => new { y.symbol, y.articledatetime, y.headline, y.NewsId });

            modelBuilder.Entity<LatestDate>().HasKey(y => y.symbol);
            modelBuilder.Entity<UserAccount>().HasKey(y => y.UserName);
            modelBuilder.Entity<UserDepotMainObject>().HasKey(y => y.DepotId);
            modelBuilder.Entity<UserDepotF>().HasKey(y => y.DepotId);
            modelBuilder.Entity<UserDepotComponents>().HasKey(y => new { y.DepotComponentId});
            modelBuilder.Entity<UserDepotsView>().HasKey(y => new { y.DepotId , y.Symbol, y.DepotValidFrom});
        }
    }
}
