using System;

namespace Byui.Something.ApplicationCore.Common.Models
{
    public class PagingOptions
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }

        public int RecordsToSkip(int count)
        {
            if (count == 0)
                return 0;

            var skip = (Page - 1) * PageSize;
            if (skip > count)
            {
                var pageCount = (int)Math.Ceiling(count / (double)PageSize);
                Page = pageCount - 1;
                skip = (Page - 1) * PageSize;
            }
            return skip;
        }
    }
}
