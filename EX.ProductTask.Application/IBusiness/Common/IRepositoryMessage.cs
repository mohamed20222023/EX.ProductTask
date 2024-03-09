using System;
using Application.Dtos.Message;
using Core.Common.Dto;

namespace Application.IBusiness.Common;
public interface IRepositoryMessage
{
RepositoryMessage SuccessMessage(object returnEntity = null, string msg = "",Int16 StatusCode=400);
RepositoryMessage ErrorMessage(string msg, object returnEntity = null,Int16 StatusCode=400);
RepositoryMessage ErrorMessageValidation(string msg, object returnEntity = null,Int16 StatusCode=400);
}