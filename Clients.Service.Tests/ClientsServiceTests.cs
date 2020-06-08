using System;
using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using Client.Services.Tests.Models;
using Clients.Domain.Models;
using Clients.Domain.Services;
using Xunit;
using Xunit.Priority;

namespace Clients.Service.Tests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class ClientsServiceTests : IClassFixture<IoCFixture>
    {
        private readonly IClientService _clientService;
        public const int idNotSupported = 9999;
        private readonly int _testClientId = new Random().Next(1, 10);
        private readonly int _testUnsupportedClientId = idNotSupported;

        public ClientsServiceTests(IoCFixture fixture)
        {
            _clientService = fixture.Container.Resolve<IClientService>();
        }

        [Fact]
        public async Task GetClientAsync_Should_Succeed()
        {
            var student = await _clientService.GetClientAsync(_testClientId);

            Guid.Parse(student.firstName);
            Guid.Parse(student.lastName);
        }

        [Fact]
        public async Task GetClientAsync_Should_Fail()
        {
            Func<Task<IClientModel>> getUnSupportedClient = async () => await _clientService.GetClientAsync(_testUnsupportedClientId);

            var unsupportedResult = await Task.Run(() => getUnSupportedClient);
            unsupportedResult.Should().Throw<Exception>();
        }

        [Fact]
        public async Task AddClientAsync_Should_Succeed() 
        {
            var clientId = await _clientService.AddClientAsync(new TestClientModel());

            clientId.Should().Be(Constants.AddClientAsyncMockResult);
        }

        [Fact]
        public async Task DeleteClientAsync_Should_Succeed() 
        {
            await _clientService.DeleteClientAsync(_testClientId);
        }

        [Fact]
        public async Task UndeleteClientAsync_Should_Succeed() 
        {
            await _clientService.UndeleteClientAsync(_testClientId);
        }

        [Fact]
        public async Task UndeleteClientAsync_Should_Fail()
        {
            Func<Task> undeleteUnSupportedClient = async () => await _clientService.UndeleteClientAsync(_testUnsupportedClientId);

            var unSupportedResult = await Task.Run(() => undeleteUnSupportedClient);
            unSupportedResult.Should().Throw<Exception>();
        }

    }
}