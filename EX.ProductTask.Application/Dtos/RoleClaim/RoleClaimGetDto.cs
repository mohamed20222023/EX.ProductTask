using Core.Common;

namespace Application.Dtos.Auth.RoleClaim;
public class RoleClaimGetDto:IBaseId
{
     public string NameAr { get; set; }
    public string NameEn { get; set; }
    public int ScreenAppId { get; set; }
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
}
