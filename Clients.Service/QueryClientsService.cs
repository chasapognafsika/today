using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Clients.Domain.Models;
using Clients.Domain.Providers;
using Clients.Domain.Services;

namespace Client.Services
{
    public class QueryClientsService : IQueryClientsService
    {
        IQueryClientsProvider _provider;

        public QueryClientsService(IQueryClientsProvider provider)
        {
            _provider = provider;
        }

        public async Task<IQueryable<IClientModel>> QueryClientsAsync()
        {
            return await _provider.QueryClientsAsync();
        }
    }
}