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

        public ClientService(IClientProvider provider)
        {
            _provider = provider;
        }

        public async Task<IClientModel> GetClientAsync(int id)
        {
            if (id == 666)
                throw new Exception("Evil IDs are not supported.");

            return await _provider.GetClientAsync(id);
        }

        public async Task<int> AddClientAsync(IClientModel student)
        {
            return await _provider.AddClientAsync(student);
        }

        public async Task UpdateClientAsync(IClientModel student)
        {
            //if (student.birthDate >= DateTime.UtcNow)
            //    throw new Exception("Invalid birth date.");

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
            if (id == 666)
                throw new Exception("Evil IDs cannot be revived.");

            var student = await _provider.GetClientAsync(id);
            student.isDeleted = false;

            await _provider.UpdateClientAsync(student);
        }

    }
}