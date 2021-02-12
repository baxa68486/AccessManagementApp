using System.Collections.Generic;

namespace DataAccessLayer.Pagination
{
    public interface IPaginationHandler<T>
    {
        ResponseModel<T> Create(IEnumerable<T> source, int pageNumber, int pageSize, int count);
    }
}
