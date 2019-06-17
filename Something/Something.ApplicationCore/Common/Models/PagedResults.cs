using System;
using System.Collections.Generic;

namespace Byui.Something.ApplicationCore.Common.Models
{
    public class PagedResults<T>
    {
        public List<T> Items { get; set; }
        public int Count { get; set; }
        public int PageCount { get; set; }
        public PagingOptions PagingOptions { get; set; }
        
        public int RecordsToSkip()
        {
            return PagingOptions.RecordsToSkip(Count);
        }

        public void SetPageCount()
        {
            if (PagingOptions.PageSize == 0)
            {
                return;
            }
            PageCount = (int)Math.Ceiling((double)Count / PagingOptions.PageSize);
        }
    }
}
