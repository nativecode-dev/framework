namespace NativeCode.Core.Types.Mappings
{
    public interface IMappingProvider
    {
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination) where TSource : class where TDestination : class;

        TDestination Map<TSource, TDestination>(TSource source) where TSource : class where TDestination : class;
    }
}