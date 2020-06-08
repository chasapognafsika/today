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
        private readonly IClientService _studentService;
        
        private readonly int _testClientId = new Random().Next(1, 10);
        private readonly int _testEvilClientId = 666;

        public ClientsServiceTests(IoCFixture fixture)
        {
            _studentService = fixture.Container.Resolve<IClientService>();
        }

        [Fact]
        public async Task GetClientAsync_Should_Succeed()
        {
            var student = await _studentService.GetClientAsync(_testClientId);

            Guid.Parse(student.firstName);
            Guid.Parse(student.lastName);
        }

        [Fact]
        public async Task GetClientAsync_Should_Fail()
        {
            Func<Task<IClientModel>> getEvilClient = async () => await _studentService.GetClientAsync(_testEvilClientId);

            var evilResult = await Task.Run(() => getEvilClient);
            evilResult.Should().Throw<Exception>();
        }

        [Fact]
        public async Task AddClientAsync_Should_Succeed() 
        {
            var studentId = await _studentService.AddClientAsync(new TestClientModel());
            
            studentId.Should().Be(Constants.AddClientAsyncMockResult);
        }

        [Fact]
        public async Task DeleteClientAsync_Should_Succeed() 
        {
            await _studentService.DeleteClientAsync(_testClientId);
        }

        [Fact]
        public async Task UndeleteClientAsync_Should_Succeed() 
        {
            await _studentService.UndeleteClientAsync(_testClientId);
        }

        [Fact]
        public async Task UndeleteClientAsync_Should_Fail()
        {
            Func<Task> undeleteEvilClient = async () => await _studentService.UndeleteClientAsync(_testEvilClientId);

            var evilResult = await Task.Run(() => undeleteEvilClient);
            evilResult.Should().Throw<Exception>();
        }

    }
}