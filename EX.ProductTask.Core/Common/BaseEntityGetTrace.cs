using System;
namespace Core.Common;
public class BaseEntityGetTrace : BaseId
{
    public DateTime CreatedDate { get; set; }
    public DateTime LastEditDate { get; set; }
  public Guid? ClientId { get; set; }  // forign key for index

}
