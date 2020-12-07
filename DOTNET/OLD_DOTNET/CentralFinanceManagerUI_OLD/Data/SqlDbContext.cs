using Microsoft.EntityFrameworkCore;
using CentralFinanceManagerUI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CentralFinanceManagerUI.Models.UserDepots;
using CentralFinanceManagerUI.Models.SymbolViewModels;

namespace CentralFinanceManagerUI.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {
        }

        public DbSet<V_DepotComponentsAggregated> V_DepotComponentsAggregated { get; set; }

        public DbSet<V_MARKET_DATA_EOD> V_MARKET_DATA_EODs { get; set; }

        public DbSet<UserDepot> UserDepot { get; set; }

        public DbSet<UserDepotObj> UserDepotObjs { get; set; }

        public DbSet<DepotComponents> DepotComponents { get; set; }

        public DbSet<Symbols> Symbols { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<V_DepotComponentsAggregated>().Property(v => v.DepotComponentId).HasColumnType("INT");

            modelBuilder.Entity<V_DepotComponentsAggregated>().Property(v => v.DepotId).HasColumnType("INT");

            modelBuilder.Entity<V_DepotComponentsAggregated>().Property(v => v.UserHK).HasColumnType("VARCHAR(256)");

            modelBuilder.Entity<V_DepotComponentsAggregated>().Property(v => v.ValidFrom).HasColumnType("DateTime");

            modelBuilder.Entity<V_DepotComponentsAggregated>().Property(v => v.ValidTo).HasColumnType("DateTime");

            modelBuilder.Entity<V_DepotComponentsAggregated>().Property(v => v.Symbol).HasColumnType("VARCHAR(256)");

            modelBuilder.Entity<V_DepotComponentsAggregated>().Property(v => v.Quantity).HasColumnType("INT");

            modelBuilder.Entity<V_DepotComponentsAggregated>().Property(v => v.PositionValueThen).HasColumnType("DECIMAL(37,17)");

            modelBuilder.Entity<V_DepotComponentsAggregated>().Property(v => v.PositionValueNow).HasColumnType("DECIMAL(37,17)");

            modelBuilder.Entity<V_DepotComponentsAggregated>().Property(v => v.Percentage).HasColumnType("DECIMAL(37,17)");



            modelBuilder.Entity<UserDepotObj>().Property(a => a.DepotId).HasColumnType("INT");
            modelBuilder.Entity<UserDepotObj>().Property(a => a.UserHK).HasColumnType("nvarchar(256)");

            modelBuilder.Entity<UserDepot>().Property(a => a.DepotId).HasColumnType("INT");
            modelBuilder.Entity<UserDepot>().Property(a => a.UserHK).HasColumnType("nvarchar(256)");
            modelBuilder.Entity<UserDepot>().Property(a => a.ValidFrom).HasColumnType("DateTime");
            modelBuilder.Entity<UserDepot>().Property(a => a.ValidTo).HasColumnType("DateTime");
            modelBuilder.Entity<UserDepot>().Property(a => a.DepotName).HasColumnType("VARCHAR(512)");

            modelBuilder.Entity<V_MARKET_DATA_EOD>().Property(a => a.ColumnID).HasColumnType("int");
            modelBuilder.Entity<V_MARKET_DATA_EOD>().Property(a => a.Symbol).HasColumnType("varchar(256)");
            modelBuilder.Entity<V_MARKET_DATA_EOD>().Property(a => a.Open).HasColumnType("decimal(38,17)");
            modelBuilder.Entity<V_MARKET_DATA_EOD>().Property(b => b.High).HasColumnType("decimal(38,17)");
            modelBuilder.Entity<V_MARKET_DATA_EOD>().Property(b => b.Low).HasColumnType("decimal(38,17)");
            modelBuilder.Entity<V_MARKET_DATA_EOD>().Property(b => b.Close).HasColumnType("decimal(38,17)");
            modelBuilder.Entity<V_MARKET_DATA_EOD>().Property(b => b.AGG_Volume).HasColumnType("bigint");
            modelBuilder.Entity<V_MARKET_DATA_EOD>().Property(b => b.Market_Timestamp).HasColumnType("datetime");
            modelBuilder.Entity<V_MARKET_DATA_EOD>().Property(a => a.Name).HasColumnType("varchar(256)");
        }

        public DbSet<CentralFinanceManagerUI.Models.F_MARKET_DATA> F_MARKET_DATA { get; set; }

    }
}

