using Application.Common.Pagination;
using AutoMapper;
using Core.Common;
using Core.Common.Dto;
using Core.Interfaces;
using Core.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Application.Dtos.Message;
using Application.IBusiness.Common;
using FluentValidation.Results;
using Application.Services;
using System.Linq.Expressions;
using Core.Exceptions;

namespace Application.Business.Common;
public class EntitiesBusinessCommon<T, TDtoGet, TRegister, TEdit, TPagination> :
IEntitiesBusinessCommon<T, TDtoGet, TRegister, TEdit, TPagination>
where T : BaseEntity
where TDtoGet : class
where TRegister : class
where TEdit : class
where TPagination : IPaginationParam
{
    #region  init
    const int d=0;
    protected readonly IRepositoryApp<T> _repo;
    protected readonly IMapper _mapper;
    protected readonly IHttpContextAccessor _accssor;
    protected readonly IRepositoryMessage _iRepositoryMessage;
    protected readonly IClockService _iClockService;
    public EntitiesBusinessCommon(
    IRepositoryApp<T> Repo,
    IMapper mapper,
    IHttpContextAccessor accssor,
   IRepositoryMessage IRepositoryMessage,
   IClockService iClockService)
    {
        _repo = Repo;
        _mapper = mapper;
        _accssor = accssor;
        _iRepositoryMessage = IRepositoryMessage;
        _iClockService = iClockService;
    }
    #endregion

    public virtual async Task<ApiResponse> Get( TPagination paginationParam)
    {
        var entities = _repo.GetAll();
        var entitiesMapped = _mapper.ProjectTo<TDtoGet>(entities);
        var PagedList = await PagedList<TDtoGet>.CreateAsync(entitiesMapped, paginationParam.pageNumber, paginationParam.PageSize);
        // _logger.Info<T>(MessageReturn.Common_SearchFor, paramFilter);
        return new ApiResponse{Data=PagedList,StatusCode=200,Success=true};
    }
    public virtual async Task<ApiResponse> GetAll(TPagination paginationParam)
    {
        var entities = _repo.GetAll();
        entities.OrderBy(a => a.CreatedDate);
        var entitiesMapped = _mapper.ProjectTo<TDtoGet>(entities);
        var newEntitiesDto = await entitiesMapped.ToListAsync();
        return new ApiResponse{Data=newEntitiesDto,StatusCode=200,Success=true};
    }
    public virtual async Task<ApiResponse> GetAllList()
    {
        var repo = _repo.GetAll().AsNoTracking();
        var entities = await _mapper.ProjectTo<BaseListDto>(repo).ToListAsync();
        return new ApiResponse{Data=entities,StatusCode=200,Success=true};
    }
    public virtual async Task<TDtoGet> GetByIdAsync(int id)
    {
        var entity = _repo.GetAll(a => a.Id == id);
        var entitiesMapped = _mapper.ProjectTo<TDtoGet>(entity);
        var result = await entitiesMapped.FirstOrDefaultAsync();
        if (result == null)
            throw new ExceptionCommonReponse(MessageReturn.Common_NotFound, 400);
        return result;
    }
    public virtual async Task<double> GetCount()
    {
        return await _repo.Count();
    }
    public virtual async Task Register(TRegister TRegister)
    {
        var entity = _mapper.Map<T>(TRegister);
        LogRowRegister(ref entity);
        _repo.Add(entity);
        await _repo.SaveAllAsync();
    }
      public virtual void RegisterNoAsync(TRegister TRegister)
    {
        var entity = _mapper.Map<T>(TRegister);
        LogRowRegister(ref entity);
        _repo.Add(entity);
          _repo.SaveAll();
    }
    public virtual async Task Edit(int id, TEdit entityEdit)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new ExceptionCommonReponse(MessageReturn.Common_NotFound, 400);
        var entityOld = _mapper.Map(entityEdit, entity);
        LogRowEdit(ref entityOld);
        _repo.Update(entityOld);
        await _repo.SaveAllAsync();
    }
    public virtual async Task DeleteById(int id)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new ExceptionCommonReponse(MessageReturn.Common_NotFound, 400);
        _repo.Delete(entity);
        await _repo.SaveAllAsync();
    }
    public virtual async Task DeleteRangeSoft(int[] arrayObjects)
    {
        List<T> entities = new List<T>();
        foreach (var id in arrayObjects)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                LogRowEdit(ref entity);
                entities.Add(entity);
            }
        }
        if (entities.Any())
        {
            _repo.UpdateRange(entities);
            await _repo.SaveAllAsync();
        }
    }
    public virtual async Task DeleteRange(int[] arrayObjects)
    {
        List<T> entities = new();
        foreach (var id in arrayObjects)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
                entities.Add(entity);
        }
        if (entities.Any())
        {
            _repo.DeleteRange(entities);
            await _repo.SaveAllAsync();
        }
    }

    public virtual async Task DeleteSoftById(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            throw new ExceptionCommonReponse(MessageReturn.Common_NotFound, 400);
        entity.IsDeleted = true;
        LogRowEdit(ref entity);

        _repo.Update(entity);
        await _repo.SaveAllAsync();
    }
    protected void LogRowRegister(ref T entity)
    {
        var userName = _accssor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        entity.CreatedDate = entity.LastEditDate = _iClockService.Now;
        entity.UserCreatedName = entity.UserLastEditName = userName;
        entity.ClientId = entity.ClientId ?? Guid.NewGuid();
    }
    protected void LogRowEdit(ref T entity)
    {
        var userName = _accssor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        entity.UserLastEditName = userName;
        entity.LastEditDate = _iClockService.Now;
    }

    protected RepositoryMessage ErrorMessageValidation(ValidationResult validationResult)
    {
        var message = string.Join(Environment.NewLine, validationResult.Errors.ToList());
        return _iRepositoryMessage.ErrorMessageValidation(message);
    }


}
