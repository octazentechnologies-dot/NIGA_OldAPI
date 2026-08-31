using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class PaginationResult
    {
        public double TotalCount { get; set; } = 0;
        public double TotalPageCount { get; set; } = 0;

        public Object ResultObject { get; set; }
    }
}
