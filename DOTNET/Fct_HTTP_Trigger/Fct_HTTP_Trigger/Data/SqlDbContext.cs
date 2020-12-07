using System;
using Fct_HTTP_Trigger.Model;
using Microsoft.EntityFrameworkCore;

namespace Fct_HTTP_Trigger.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {
        }

        public DbSet<API_TRIGGER> API_TRIGGERS { get; set; }

        public DbSet<API_CALL> API_CALLS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<API_CALL>().HasKey(y => y.API_CALLS);

            modelBuilder.Entity<API_TRIGGER>().HasKey(y => y.ApiId);
            modelBuilder.Entity<API_TRIGGER>().Property(a => a.ApiId).HasColumnType("INT");
            modelBuilder.Entity<API_TRIGGER>().Property(a => a.ApiView).HasColumnType("varchar(256)");

            modelBuilder.Entity<API_TRIGGER>().Property(a => a.ApiAddress).HasColumnType("varchar(256)");

            modelBuilder.Entity<API_TRIGGER>().Property(a => a.ValidFrom).HasColumnType("DateTime");
            modelBuilder.Entity<API_TRIGGER>().Property(a => a.ValidTo).HasColumnType("DateTime");
            modelBuilder.Entity<API_TRIGGER>().Property(a => a.ITS).HasColumnType("DateTime");
            modelBuilder.Entity<API_TRIGGER>().Property(a => a.UTS).HasColumnType("DateTime");
        }

        internal int Table(string v)
        {
            throw new NotImplementedException();
        }
    }
}
