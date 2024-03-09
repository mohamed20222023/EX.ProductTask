using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Pagination
{
    public interface IPaginationParam
    {

        int pageNumber { get; set; }
        int PageSize { get; set; }

    }
}