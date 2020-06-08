using Clients.Domain.Models;
using System.Threading.Tasks;

namespace Clients.Domain.Providers
{
    public interface IClientProvider
    {
        Task<IClientModel> GetClientAsync(int id);

        Task<int> AddClientAsync(IClientModel student);

        Task UpdateClientAsync(IClientModel student);

        Task DeleteClientAsync(int id);
    }
}