using System;

namespace Application.Dtos.Auth.roles;
public class UserPermisionEditDto
{
    public Guid UserId { get; set; }
    public List<UserClaimPermision> Permisions { get; set; }

}

