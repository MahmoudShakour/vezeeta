using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public class Pagination
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public int GetSkip()
        {
            return (PageNumber - 1) * PageSize;
        }

        public int GetTake()
        {
            return PageSize;
        }
    }
}