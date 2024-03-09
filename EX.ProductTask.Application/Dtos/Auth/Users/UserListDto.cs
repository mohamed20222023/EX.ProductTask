using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Core.Enums;

namespace Application.Dtos.Users
{
  public class UserListDto : BaseEntityGetTrace
  {
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime? LastLogin { get; set; }
    public string UserType { get; set; }
    public string URL { get; set; }

  }
}
