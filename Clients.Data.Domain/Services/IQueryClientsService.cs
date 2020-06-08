using Clients.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Clients.Domain.Services
{
    public interface IQueryClientsService
    {
        IQueryable<IClientModel> QueryClientsAsync(Expression<Func<IClientModel, bool>> filter, int? skip, int? top);
    }
}