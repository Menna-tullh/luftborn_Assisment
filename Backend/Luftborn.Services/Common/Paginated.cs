using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftborn.Services.Common
{
    public class Paginated<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int pageIndex { get; set; }
        public List<T>? Data { get; set; }
    }
}
