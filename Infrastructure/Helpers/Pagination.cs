using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public class Pagination
    {
        public int PageSize { get; set; } =10;
        public int PageNumber { get; set; }=1;

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