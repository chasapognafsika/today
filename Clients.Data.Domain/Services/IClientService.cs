using Clients.Domain.Models;
using System.Threading.Tasks;

namespace Clients.Domain.Services
{
    public interface IClientService
    {
        Task<IClientModel> GetClientAsync(int id);

        Task<int> AddClientAsync(IClientModel student);

        Task UpdateClientAsync(IClientModel student);

        Task DeleteClientAsync(int id);

        Task UndeleteClientAsync(int id);
    }
}