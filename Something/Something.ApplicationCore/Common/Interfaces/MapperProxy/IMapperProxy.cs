namespace Byui.Something.ApplicationCore.Common.Interfaces.MapperProxy
{
    public interface IMapperProxy
    {
        TDestination Map<TDestination>(object source);
        void Map(object source, object destination);
    }
}