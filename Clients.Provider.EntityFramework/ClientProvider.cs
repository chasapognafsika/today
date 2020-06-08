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
        public IDbContext db { get; }

        public ClientProvider(IDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<IClientModel> GetClientAsync(int id)
        {
            var entity = await db.Clients.FirstOrDefaultAsync(s => s.id == id);

            return entity.ToClientModel();
        }

        public async Task<int> AddClientAsync(IClientModel client)
        {
            var entity = client.ToClientEntity();

            var addAsyncResult = await db.Clients.AddAsync(entity);
            await db.SaveChangesAsync();

            return addAsyncResult.Entity.id;
        }

        public async Task UpdateClientAsync(IClientModel client)
        {
            var entity = await db.Clients.FindAsync(client.id);
            db.Entry(entity).CurrentValues.SetValues(client.ToClientEntity());

            db.Clients.Update(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var student = await db.Clients.FindAsync(id);

            db.Clients.Remove(student);
            await db.SaveChangesAsync();
        }
    }
}