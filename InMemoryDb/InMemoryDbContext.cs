using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Clients.Data;
using Clients.Data.Entities;

namespace InMemoryDb
{
    public class InMemoryDbContext : DbContext, IDbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }

        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options): base(options)
        { }

        public InMemoryDbContext()
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
            optionsBuilder.UseInMemoryDatabase(config.GetConnectionString("InMemoryDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}