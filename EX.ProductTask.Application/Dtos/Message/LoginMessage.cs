using System;

namespace Application.Dtos.Message;
public class LoginMessage
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public object ReturnEntity { get; set; }

    public Int16 StatusCode { get; set; }

}
