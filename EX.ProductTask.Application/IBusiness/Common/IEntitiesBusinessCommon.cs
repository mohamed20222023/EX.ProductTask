using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Message;
using Core.Common;
using Core.Common.Dto;
using global::Application.Common.Pagination;
using Microsoft.AspNetCore.Http;

namespace Application.IBusiness.Common;
public interface IEntitiesBusinessCommon<T, TDtoGet, TRegister, TEdit, TPagination>
 where T : IBaseEntity  
 where TDtoGet : class
 where TRegister : class
where TEdit : class
where TPagination : IPaginationParam
{
    Task<ApiResponse> Get(TPagination paginationParam);
    Task<ApiResponse> GetAll(TPagination paginationParam);
    Task<ApiResponse> GetAllList();
     Task<TDtoGet> GetByIdAsync(int id);
    Task Register(TRegister TRegister);
    void RegisterNoAsync(TRegister TRegister);
    Task<double> GetCount();
    Task  Edit(int id, TEdit entityEdit);
    Task DeleteById(int id);
    Task DeleteSoftById(int id);
    Task DeleteRangeSoft(int[] arrayObjects);
    Task DeleteRange(int[] arrayObjects);
}
