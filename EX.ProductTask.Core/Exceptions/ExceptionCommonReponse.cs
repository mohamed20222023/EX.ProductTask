using System;
namespace Core.Exceptions;
public class ExceptionCommonReponse : Exception
{
    public int StatusCode { get; set; }
    public bool  IsCustome { get; set; }=true;
     public ExceptionCommonReponse(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}

