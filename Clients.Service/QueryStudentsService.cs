using System;
using System.Linq;
using System.Linq.Expressions;
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

        public IQueryable<IClientModel> QueryClientsAsync(Expression<Func<IClientModel, bool>> filter, int? skip, int? top)
        {
            return _provider.QueryClientsAsync(filter, skip, top);
        }
    }
}