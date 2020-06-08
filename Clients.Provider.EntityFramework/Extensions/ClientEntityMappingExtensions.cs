using System.Collections.Generic;
using AutoMapper;
using Clients.Data.Entities;
using Clients.Domain.Models;
using Clients.Provider.EntityFramework.Mapping;

namespace Clients.Provider.EntityFramework.Extensions
{
    public static class ClientEntityMappingExtensions
    {
        public static IClientModel ToClientModel(this ClientEntity entity)
        {
            IMapper mapper = MappingConfiguration.instance.CreateMapper();
            return mapper.Map<IClientModel>(entity);
        }

        public static IEnumerable<IClientModel> ToClientModels(this IEnumerable<ClientEntity> entity)
        {
            IMapper mapper = MappingConfiguration.instance.CreateMapper();
            return mapper.Map<IEnumerable<IClientModel>>(entity);
        }
    }
}