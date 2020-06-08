using System.Threading.Tasks;
using Clients.Data;
using Clients.Domain.Models;
using Clients.Domain.Providers;
using Clients.Provider.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Clients.Provider.EntityFramework
{
    public class ClientProvider : IClientProvider
    {
        public IDbContext _dbContext { get; }

        public ClientProvider(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IClientModel> GetClientAsync(int id)
        {
            var entity = await _dbContext.Clients.FirstOrDefaultAsync(s => s.id == id);

            return entity.ToClientModel();
        }

        public async Task<int> AddClientAsync(IClientModel client)
        {
            var entity = client.ToClientEntity();

            var addAsyncResult = await _dbContext.Clients.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return addAsyncResult.Entity.id;
        }

        public async Task UpdateClientAsync(IClientModel client)
        {
            var entity = await _dbContext.Clients.FindAsync(client.id);
            _dbContext.Entry(entity).CurrentValues.SetValues(client.ToClientEntity());

            _dbContext.Clients.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var student = await _dbContext.Clients.FindAsync(id);

            _dbContext.Clients.Remove(student);
            await _dbContext.SaveChangesAsync();
        }
    }
}