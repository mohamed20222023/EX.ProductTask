using Application.Common.Pagination;

namespace Application.Dtos.Auth.Users;
public class UserParam 
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
