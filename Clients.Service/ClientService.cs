using System;
using System.Threading.Tasks;
using Clients.Domain.Models;
using Clients.Domain.Providers;
using Clients.Domain.Services;

namespace Client.Services
{
    public class ClientService : IClientService
    {
        IClientProvider _provider;
        public const int idNotSupported = 9999;

        public ClientService(IClientProvider provider)
        {
            _provider = provider;
        }

        public async Task<IClientModel> GetClientAsync(int id)
        {
            if (id == idNotSupported)
                throw new Exception("This ID is not supported.");

            return await _provider.GetClientAsync(id);
        }

        public async Task<int> AddClientAsync(IClientModel student)
        {
            return await _provider.AddClientAsync(student);
        }

        public async Task UpdateClientAsync(IClientModel student)
        {
            await _provider.UpdateClientAsync(student);
        }

        public async Task DeleteClientAsync(int id)
        {
            var student = await _provider.GetClientAsync(id);
            student.isDeleted = true;

            await _provider.UpdateClientAsync(student);
        }

        public async Task UndeleteClientAsync(int id)
        {
            if (id == idNotSupported)
                throw new Exception("Unsupported IDs cannot be revived.");

            var client = await _provider.GetClientAsync(id);
            client.isDeleted = false;

            await _provider.UpdateClientAsync(client);
        }

    }
}