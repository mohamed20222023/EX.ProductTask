namespace Application.Common.Pagination
{
    public class PaginationParam : IPaginationParam
    {
        private const int MaxPageSize = 100000;
        public int pageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }


    }
}