using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using Client.Services.Tests.Models;
using Xunit;
using Xunit.Priority;
using Clients.Domain.Providers;

namespace Clients.Service.Tests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class ClientsProviderTests : IClassFixture<IoCFixture>
    {
        private readonly IClientProvider _clientProvider;
        
        private readonly int _testclientId = 1;

        public ClientsProviderTests(IoCFixture fixture)
        {
            _clientProvider = fixture.Container.Resolve<IClientProvider>();
        }

        [Fact]
        public async Task GetClientAsync_Should_Succeed()
        {
            await _clientProvider.AddClientAsync(new TestClientModel());

            var client = await _clientProvider.GetClientAsync(_testclientId);

            client.firstName.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task AddClientAsync_Should_Succeed() 
        {
            var clientId = await _clientProvider.AddClientAsync(new TestClientModel());

            var client = await _clientProvider.GetClientAsync(clientId);
            client.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteClientAsync_Should_Succeed() 
        {
            var clientId = await _clientProvider.AddClientAsync(new TestClientModel());

            var client = await _clientProvider.GetClientAsync(clientId);
            client.Should().NotBeNull();

            await _clientProvider.DeleteClientAsync(clientId);

            client = await _clientProvider.GetClientAsync(clientId);
            client.Should().BeNull();
        }

        [Fact]
        public async Task UpdateClientAsync_Should_Succeed() 
        {
            var clientId = await _clientProvider.AddClientAsync(new TestClientModel());

            var client = await _clientProvider.GetClientAsync(clientId);
            client.isDeleted = !client.isDeleted;
            
            await _clientProvider.UpdateClientAsync(client);

            var expected = client.isDeleted;

            client = await _clientProvider.GetClientAsync(clientId);
            client.isDeleted.Should().Be(expected);
        }

    }
}