using System.Reflection;
using Autofac;
using InMemoryDb;
using Microsoft.Extensions.Configuration;
using Clients.Data;
using Clients.Domain.Providers;
using Clients.Provider.EntityFramework;
using SqlDb;

namespace Clients.Service.Tests
{
    public class IoCFixture
    {
        public readonly IContainer Container;

        public IoCFixture()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new ContainerBuilder();

            /* IDbContext */
            builder.RegisterType<InMemoryDbContext>()
                .As<IDbContext>()
                .InstancePerLifetimeScope();

            /* IClientProvider */
            builder.RegisterType<ClientProvider>()
                .As<IClientProvider>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IoCFixture).GetTypeInfo().Assembly);
            Container = builder.Build();
        }
    }
}