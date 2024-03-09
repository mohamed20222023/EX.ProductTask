using Core.Common;

namespace Application.Dtos.Users;
public class UserGetDto : IBaseId
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
   public string URL { get; set; }
   public string Password { get; set; }


}

