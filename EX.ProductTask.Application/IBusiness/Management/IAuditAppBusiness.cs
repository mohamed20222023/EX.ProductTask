using Application.Dtos.Auth.Audit;
using Core.Common;
using Core.Entities.Management;

namespace Application.IBusiness.Management;
public interface IAuditAppBusiness
{
    Task<ApiResponse> Get( AuditParam paginationParam);
    Task<Audit> GetByIdAsync(int id);
    Task<List<AuditList>> GetTableNameAsync();
    Task<List<AuditList>> GetStateAsync();
    Task<List<AuditList>> GetLevelAsync();
}

