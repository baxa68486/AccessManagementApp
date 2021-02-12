namespace DataAccessLayer.Pagination
{
    public class PaginationResourceParameter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}