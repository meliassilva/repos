using AutoMapper;
using Byui.Something.ApplicationCore.Common.Interfaces.MapperProxy;

namespace Byui.Something.Infrastructure.MapperProxy
{
    public class MapperProxy : IMapperProxy
    {
        private readonly IMapper _mapper;

        public MapperProxy(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public void Map(object source, object destination)
        {
            _mapper.Map(source, destination);
        }
    }
}