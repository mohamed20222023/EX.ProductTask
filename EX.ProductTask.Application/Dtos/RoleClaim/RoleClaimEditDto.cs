using Core.Common;
namespace Application.Dtos.Auth.RoleClaim;
public class RoleClaimEditDto : RoleClaimRegisterDto, IBaseId
{
    public int Id { get; set; }
}
