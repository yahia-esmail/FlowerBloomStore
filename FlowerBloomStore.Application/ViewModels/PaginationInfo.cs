using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerBloomStore.Application.ViewModels
{
    public class PaginationInfo
    {
        public int currentPage { set; get; }
        public int pageSize { set; get; }
        public int PagesCount { set; get; }
        public bool hasNext { set; get; }
        public bool hasPrevious { set; get; }
    }
}
