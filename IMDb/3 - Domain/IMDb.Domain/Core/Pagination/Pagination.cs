using System;
using System.Collections.Generic;

namespace IMDb.Domain.Core.Pagination
{
    public class Pagination<T>
    {
        public IEnumerable<T> Items { get; private set; }
        public int PageCount { get; private set; }
        public int TotalItemCount { get; private set; }
        public int PageSize { get; private set; }
        public int CurrentPage { get; private set; }

        public Pagination(IEnumerable<T> items, int totalItemCount, int pageSize, int currentPage)
        {
            Items = items;
            PageCount = (int)Math.Ceiling((double)totalItemCount / pageSize);
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
        }
    }
}
