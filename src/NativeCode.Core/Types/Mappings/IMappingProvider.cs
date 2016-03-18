﻿namespace NativeCode.Core.Types.Mappings
{
    using AutoMapper;

    public interface IMappingProvider
    {
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination) where TSource : class where TDestination : class;

        TDestination Map<TSource, TDestination>(TSource source) where TSource : class where TDestination : class;
    }

    public class AutoMappingProvider : IMappingProvider
    {
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) where TSource : class where TDestination : class
        {
            return Mapper.Map(source, destination);
        }

        public TDestination Map<TSource, TDestination>(TSource source) where TSource : class where TDestination : class
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}