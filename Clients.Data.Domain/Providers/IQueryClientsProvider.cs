using Clients.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Clients.Domain.Providers
{
    public interface IQueryClientsProvider
    {
        Task<IQueryable<IClientModel>> QueryClientsAsync();
    }
}