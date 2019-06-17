using System.Collections.Generic;

namespace Byui.StudentListApi.Business.Model
{
    public class PagedResults<T>
    {
        public List<T> Items { get; set; }
        public int Count { get; set; }
        public int PageCount { get; set; }
        public PagingOptions PagingOptions { get; set; }
    }
}
