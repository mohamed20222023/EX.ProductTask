using System;
using Application.Dtos.Message;
using Application.IBusiness.Common;
using Core.Common.Dto;
using Core.Interfaces.Common;

namespace Application.Business.Common;
public class RepositoryMessageService : IRepositoryMessage
{

    public RepositoryMessageService()
    {}
    public  RepositoryMessage ErrorMessage(string msg, object returnEntity = null,Int16 StatusCode=400)
    {
          return new RepositoryMessage
        {
            Status = false,
             Message = string.IsNullOrEmpty(msg)?"": msg,
            ReturnEntity = returnEntity,
            StatusCode=StatusCode
        };
    }
   public  RepositoryMessage ErrorMessageValidation(string msg, object returnEntity = null,Int16 StatusCode=400)
    {
          return new RepositoryMessage
        {
            Status = false,
             Message = msg,
            ReturnEntity = returnEntity,
            StatusCode=StatusCode
        };
    }
    public RepositoryMessage SuccessMessage(object returnEntity = null, string msg = "",Int16 StatusCode=400)
    {
        return new RepositoryMessage
        {
            Status = true,
            Message = string.IsNullOrEmpty(msg)?"": msg,
             ReturnEntity = returnEntity,
            StatusCode=StatusCode

        };
    }
}
