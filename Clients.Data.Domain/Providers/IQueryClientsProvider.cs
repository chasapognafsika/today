using Clients.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clients.Domain.Providers
{
    public interface IQueryClientsProvider
    {
        Task<IQueryable<IClientModel>> QueryClientsAsync();
    }
}