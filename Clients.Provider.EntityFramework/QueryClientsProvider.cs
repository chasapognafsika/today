using System;
using System.Linq;
using System.Linq.Expressions;
using Clients.Data;
using Clients.Domain.Models;
using Clients.Domain.Providers;
using Clients.Provider.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Clients.Provider.EntityFramework
{
    public class QueryClientsProvider : IQueryClientsProvider
    {
        public IDbContext db { get; }

        public QueryClientsProvider(IDbContext dbContext)
        {
            db = dbContext;
        }

        public IQueryable<IClientModel> QueryClientsAsync(Expression<Func<IClientModel, bool>> filter, int? skip, int? top)
        {
            var where = filter.ToClientEntityFilterExpression().Compile();

            var entities = db.clients
                .AsNoTracking()
                .Where(where)
                .Skip(skip ?? 0)
                .Take(top ?? int.MaxValue);

            return entities.ToClientModels().AsQueryable();
        }
    }
}