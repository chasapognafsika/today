using System;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Clients.Data.Entities;
using Clients.Domain.Models;

namespace Clients.Provider.EntityFramework.Mapping
{
    public class MappingConfiguration
    {
        private static readonly Lazy<MappingConfiguration> _instance = new Lazy<MappingConfiguration>(() => new MappingConfiguration());

        private readonly MapperConfiguration _config;

        private MappingConfiguration()
        {
            _config = new MapperConfiguration(c =>
            {
                c.AddExpressionMapping();

                c.CreateMap<IClientModel, ClientEntity>(MemberList.Source)
                    .ForMember(t => t.deleted, opts => opts.MapFrom(s => s.isDeleted));

                c.CreateMap<ClientEntity, IClientModel>(MemberList.Destination)
                    .ForMember(t => t.isDeleted, opts => opts.MapFrom(s => s.deleted));
            });

            _config.AssertConfigurationIsValid();
        }

        public static MappingConfiguration instance => _instance.Value;

        public IMapper CreateMapper() => _config.CreateMapper();
    }
}