using System.ComponentModel.DataAnnotations.Schema;
using Core.Common;

namespace Core.Entities.Management;

public class Audit:IBaseId
{
    public int Id { get; set; }
    public string RowClientId { get; set; }
     public string TableName { get; set; }
    public string State { get; set; }
    public string OldValues { get; set; }
    public string NewValues { get; set; }
    public int? PrimaryKey { get; set; }
    public string Message { get; set; }
    public string Level { get; set; }
    public DateTime TimeStamp { get; set; }
    public string ExceptionMessage { get; set; }
    public string UserName { get; set; }
    public string UserFullName { get; set; }
    public int? StatusCode { get; set; }
    public string ServerIP { get; set; }
    public string ClientIP { get; set; }

    public string ApplicationName { get; set; }
    public string QueryString { get; set; }
    public bool IsShowUser { get; set; }
    public bool IsLogin { get; set; }
    public bool IsLogout { get; set; }
    [NotMapped]
    public object PrimaryKeyObj { get; set; }


}

