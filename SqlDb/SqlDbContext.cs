using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Clients.Data;
using Clients.Data.Entities;

namespace SqlDb
{
  public class SqlDbContext : DbContext, IDbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options): base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (optionsBuilder.IsConfigured)
                return;

            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

#if DEBUG
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
#endif
            optionsBuilder.UseSqlServer(config.GetConnectionString("SqlDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


   }
}