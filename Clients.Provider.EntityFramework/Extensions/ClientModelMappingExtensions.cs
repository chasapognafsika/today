using System;
using System.Linq.Expressions;
using AutoMapper;
using Clients.Data.Entities;
using Clients.Domain.Models;
using Clients.Provider.EntityFramework.Mapping;

namespace Clients.Provider.EntityFramework.Extensions
{
    public static class ClientModelMappingExtensions
    {
        public static ClientEntity ToClientEntity(this IClientModel model)
        {
            IMapper mapper = MappingConfiguration.instance.CreateMapper();
            return mapper.Map<ClientEntity>(model);
        }

        public static Expression<Func<ClientEntity, bool>> ToClientEntityFilterExpression(this Expression<Func<IClientModel, bool>> expression)
        {
            IMapper mapper = MappingConfiguration.instance.CreateMapper();
            return mapper.Map<Expression<Func<IClientModel, bool>>, Expression<Func<ClientEntity, bool>>>(expression);
        }
    }
}