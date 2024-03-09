namespace Core.Common;
public interface IBaseEntity:IBaseId
{
    public DateTime CreatedDate { get; set; }
    public DateTime LastEditDate { get; set; }
    public string UserCreatedName { get; set; }
    public string UserLastEditName { get; set; }
    public bool IsDeleted { get; set; }
}


