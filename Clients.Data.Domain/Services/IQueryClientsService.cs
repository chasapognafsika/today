using Clients.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Clients.Domain.Services
{
    public interface IQueryClientsService
    {
        Task<IQueryable<IClientModel>> QueryClientsAsync();
    }
}