using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shared.DTOs
{
    //each time we return a paginated response, we will use this class to standardize the response structure.

    public class PaginationResponse<TEntity>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<TEntity> Data { get; set; }

        public PaginationResponse(int pageNumber, int pageSize, int count, IEnumerable<TEntity> data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
