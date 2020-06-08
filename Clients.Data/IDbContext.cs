using Clients.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clients.Data
{
    public interface IDbContext : IAbstractDbContext
    {
        DbSet<ClientEntity> clients { get; set; }
    }
}