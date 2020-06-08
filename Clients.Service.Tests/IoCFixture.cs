using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;
using Moq;
using Client.Services;
using Client.Services.Tests.Models;
using Clients.Domain.Models;
using Clients.Domain.Providers;
using Clients.Domain.Services;

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

            /* IClientProvider mock */
            var studentProviderMock = new Mock<IClientProvider>();

            studentProviderMock.Setup(provider => provider.GetClientAsync(It.IsAny<int>()))
                .ReturnsAsync(new TestClientModel());

            studentProviderMock.Setup(provider => provider.AddClientAsync(It.IsAny<IClientModel>()))
                .ReturnsAsync(Constants.AddClientAsyncMockResult);

            studentProviderMock.Setup(provider => provider.UpdateClientAsync(It.IsAny<IClientModel>()))
                .Returns(Task.CompletedTask);

            studentProviderMock.Setup(provider => provider.DeleteClientAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            builder.RegisterInstance(studentProviderMock.Object)
                .As<IClientProvider>()
                .SingleInstance();

            /* IClientService */
            builder.RegisterType<ClientService>()
                .As<IClientService>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IoCFixture).GetTypeInfo().Assembly);
            Container = builder.Build();
        }
    }
}