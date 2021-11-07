using AutoMapper;
using Politics.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Mapping
{
  //public static class PaginatedMapper
  //{
  //  public static PaginatedList<TDestination> ToMappedPagedList<TSource, TDestination>(this PaginatedList<TSource> list)
  //  {
  //    IEnumerable<TDestination> sourceList = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(list);
  //    PaginatedList<TDestination> pagedResult = new PaginatedList<TDestination>(sourceList, list.Count, list.PageIndex, list.PageSize);
  //    return pagedResult;

  //  }
  //}
  //public interface ITypeConverter<in TSource, TDestination>
  //{
  //  TDestination Convert(TSource source, TDestination destination, ResolutionContext context);
  //}
  //public class PaginatedListConverter : ITypeConverter<PaginatedList<SourceT>, PaginatedList<DestinationT>>
  //{
  //  public DateTime Convert(, ResolutionContext context)
  //  {
  //    return System.Convert.ToDateTime(source);
  //  }
  //}
}
