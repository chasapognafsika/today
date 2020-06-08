using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Clients.Data;
using Clients.Domain.Models;
using Clients.Domain.Providers;
using Clients.Provider.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Clients.Provider.EntityFramework
{
    public class QueryClientsProvider : IQueryClientsProvider
    {
        public IDbContext _dbContext { get; }

        public QueryClientsProvider(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<IClientModel>> QueryClientsAsync()
        {
            var entities = await _dbContext.Clients.ToListAsync();
            return entities.ToClientModels().AsQueryable(); 
        }
    }
}