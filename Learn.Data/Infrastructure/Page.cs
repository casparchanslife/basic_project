﻿using Learn.Core;
using System.Linq;

namespace Learn.Data.Infrastructure
{
    public class Page
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Page()
        {
            PageNumber = 1;
            PageSize = Global.CommonPageSize;
        }
     
        public Page(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int Skip
        {
            get { return (PageNumber - 1) * PageSize; }
        }
    }

    public static class PagingExtensions
    {
        /// <summary>
        /// Extend IQueryable to simplify access to skip and take methods 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="page"></param>
        /// <returns>IQueryable with Skip and Take having been performed</returns>
        public static IQueryable<T> GetPage<T>(this IQueryable<T> queryable, Page page)
        {
            return queryable.Skip(page.Skip).Take(page.PageSize);
        }
    }
}