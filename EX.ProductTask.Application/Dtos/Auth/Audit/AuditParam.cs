using Application.Common.Pagination;

namespace Application.Dtos.Auth.Audit;
public class AuditParam : PaginationParam
{
    public string[] TablesName { get; set; }=new string[]{};
    public string[] Levels { get; set; }=new string[]{};
    public string[] States { get; set; }=new string[]{};
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string UserFullName { get; set; }
    public string RowClientId { get; set; }
    public int? StatusCode { get; set; }
    public bool IsShowUser { get; set; }
    public bool IsLogin { get; set; }
    public bool IsLogout { get; set; }

}
