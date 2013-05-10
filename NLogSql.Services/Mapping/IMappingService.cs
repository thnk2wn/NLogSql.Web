using System;
using System.Collections.Generic;

namespace NLogSql.Services.Mapping
{
    public interface IMappingService
    {
        TDestination Map<TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

        // could have this as an overload of Map but I think that gets confusing
        IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source);

        object Map(object source, Type sourceType, Type destinationType);

        object Map(object source, object destination, Type sourceType, Type destinationType);
    }
}
