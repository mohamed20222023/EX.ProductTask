using System;

namespace Application.Dtos.Auth.roles;
public class RolePermisionEditDto
{
    public int RoleId { get; set; }
    public List<RoleClaimPermision> Permisions { get; set; }

}

