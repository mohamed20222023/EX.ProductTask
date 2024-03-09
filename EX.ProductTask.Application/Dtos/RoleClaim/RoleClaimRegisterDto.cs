using Microsoft.AspNetCore.Identity;

namespace Application.Dtos.Auth.RoleClaim;
public class RoleClaimRegisterDto
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public int ScreenAppId { get; set; }
    public int RoleId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
    public string ScreenApName { get; set; }
    public string RoleName { get; set; }
}
