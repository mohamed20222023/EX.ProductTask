using System;
using Core.Common;
namespace Application.Dtos.Auth.Audit;
public class AuditGetDto : IBaseId
{
    public int Id { get; set; }
    public string TableName { get; set; }
    public string State { get; set; }
     public string Message { get; set; }
    public DateTime TimeStamp { get; set; }
      public string UserFullName { get; set; }
      public string Level { get; set; }

}

