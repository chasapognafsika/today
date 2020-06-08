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
        private readonly IClientProvider _studentProvider;
        
        private readonly int _testClientId = 1;

        public ClientsProviderTests(IoCFixture fixture)
        {
            _studentProvider = fixture.Container.Resolve<IClientProvider>();
        }

        [Fact]
        public async Task GetClientAsync_Should_Succeed()
        {
            await _studentProvider.AddClientAsync(new TestClientModel());

            var student = await _studentProvider.GetClientAsync(_testClientId);

            student.firstName.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task AddClientAsync_Should_Succeed() 
        {
            var studentId = await _studentProvider.AddClientAsync(new TestClientModel());

            var student = await _studentProvider.GetClientAsync(studentId);
            student.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteClientAsync_Should_Succeed() 
        {
            var studentId = await _studentProvider.AddClientAsync(new TestClientModel());

            var student = await _studentProvider.GetClientAsync(studentId);
            student.Should().NotBeNull();

            await _studentProvider.DeleteClientAsync(studentId);

            student = await _studentProvider.GetClientAsync(studentId);
            student.Should().BeNull();
        }

        [Fact]
        public async Task UpdateClientAsync_Should_Succeed() 
        {
            var studentId = await _studentProvider.AddClientAsync(new TestClientModel());

            var student = await _studentProvider.GetClientAsync(studentId);
            student.isDeleted = !student.isDeleted;
            
            await _studentProvider.UpdateClientAsync(student);

            var expected = student.isDeleted;

            student = await _studentProvider.GetClientAsync(studentId);
            student.isDeleted.Should().Be(expected);
        }

    }
}