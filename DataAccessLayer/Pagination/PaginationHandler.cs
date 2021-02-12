using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Pagination
{
    public class PaginationHandler<T> : IPaginationHandler<T>
    {
        public ResponseModel<T> Create(IEnumerable<T> source, int pageNumber, int pageSize, int count)
        {
            if (count <= pageSize)
            {
                pageNumber = 1;
            }
            return new ResponseModel<T>(source, count, pageNumber, pageSize);
        }
    }
}