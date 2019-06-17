using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Byui.StudentListApi.Business.Enums;
using Byui.StudentListApi.Business.Model;

namespace Byui.StudentListApi.Business.Utilities
{
    /// <summary>
    /// Helps us fill our paging results.
    /// </summary>
    internal static class PagingHelper
    {
        /// <summary>
        /// Gets the paged results by applying the requested paging parameters.  You must have applied your sort before calling this method.
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <param name="queryable"></param>
        public static PagedResults<T> GetPagedResults<T>(PagingOptions pagingOptions, IQueryable<T> queryable)
        {
            var results = new PagedResults<T>() { PagingOptions = pagingOptions };
            int totalCount = queryable.Count();
            results.Count = totalCount;
            results.PageCount = (totalCount / pagingOptions.PageSize) + ((totalCount % pagingOptions.PageSize == 0) ? 0 : 1);
            results.Items = queryable.Skip(pagingOptions.RecordsToSkip()).Take(pagingOptions.PageSize).ToList();

            return results;
        }

        /// <summary>
        /// Gets the paged results by applying the requested paging parameters.  You must have applied your sort before calling this method.
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <param name="queryable"></param>
        public static PagedResults<T2> GetPagedResults<T, T2>(PagingOptions pagingOptions, IQueryable<T> queryable)
        {
            var results = new PagedResults<T2> { PagingOptions = pagingOptions };
            int totalCount = queryable.Count();
            results.Count = totalCount;
            results.PageCount = (totalCount / pagingOptions.PageSize) + ((totalCount % pagingOptions.PageSize == 0) ? 0 : 1);
            var items = queryable.Skip(pagingOptions.RecordsToSkip()).Take(pagingOptions.PageSize).ToList();
            results.Items = Mapper.Map<List<T2>>(items);

            return results;
        }

        /// <summary>
        /// Gets default paging options that can be used.
        /// </summary>
        /// <returns></returns>
        public static PagingOptions GetDefaultPagingOptions()
        {
            return new PagingOptions()
            {
                PageSize = 25,
                Page = 1,
                SortBy = "",
                SortDirection = SortDirection.Asc
            };
        }
    }

}
