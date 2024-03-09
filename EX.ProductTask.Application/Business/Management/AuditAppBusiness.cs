using Application.Common.Pagination;
using Application.Dtos.Auth.Audit;
using Application.IBusiness.Common;
using Application.IBusiness.Management;
using Application.Services;
using AutoMapper;
using Core.Common;
using Core.Entities.Management;
using Core.Exceptions;
using Core.Interfaces.Common;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Business.Management;
public class AuditAppBusiness : IAuditAppBusiness
{
    protected readonly RepositoryAppHepler<Audit> _repo;
    protected readonly IMapper _mapper;
    protected readonly IHttpContextAccessor _accssor;
    protected readonly ILogCustom _logger;
    protected readonly IRepositoryMessage _iRepositoryMessage;
    protected readonly IClockService _iClockService;
    public AuditAppBusiness(
   RepositoryAppHepler<Audit> Repo,
   IMapper mapper,
   IHttpContextAccessor accssor,
   ILogCustom logger,
  IRepositoryMessage IRepositoryMessage,
  IClockService iClockService)
    {
        _repo = Repo;
        _mapper = mapper;
        _accssor = accssor;
        _logger = logger;
        _iRepositoryMessage = IRepositoryMessage;
        _iClockService = iClockService;
    }
    public async Task<ApiResponse> Get(AuditParam paginationParam)
    {
        var entities = _repo.GetAll();
        var entitiesMapped = _mapper.ProjectTo<AuditGetDto>(entities);

        var PagedList = await PagedList<AuditGetDto>.CreateAsync(entitiesMapped, paginationParam.pageNumber, paginationParam.PageSize);
        return new ApiResponse() { Data = PagedList , StatusCode = 200 , Success = true };
    }
    public async Task<Audit> GetByIdAsync(int id)
    {
        var entity = await _repo.SingleOrDefaultAsNoTrackingAsync(a => a.Id == id);
        if (entity == null)
            throw new ExceptionCommonReponse(MessageReturn.Common_NotFound, 400);
        return entity;
    }
    public async Task<List<AuditList>> GetTableNameAsync()
    {
        return await _repo.GetAll().AsNoTracking().Select(a => new AuditList { Id = a.TableName, Name = a.TableName }).Distinct().ToListAsync();
    }
    public async Task<List<AuditList>> GetStateAsync()
    {
        return await _repo.GetAll().AsNoTracking().Select(a => new AuditList { Id = a.State, Name = a.State }).Distinct().ToListAsync();
    }
    public async Task<List<AuditList>> GetLevelAsync()
    {
        return await _repo.GetAll().AsNoTracking().Select(a => new AuditList { Id = a.Level, Name = a.Level }).Distinct().ToListAsync();
    }
    public virtual void Filter(ref IQueryable<Audit> entities, AuditParam paginationParam)
    {
         if (paginationParam.DateFrom != null && paginationParam.DateTo != null )
            entities = entities.Where(a =>a.TimeStamp>=paginationParam.DateFrom&&a.TimeStamp<=paginationParam.DateFrom);
         if (!string.IsNullOrEmpty(paginationParam.UserFullName))
            entities = entities.Where(a => a.UserFullName.Contains(paginationParam.UserFullName));
        if (!string.IsNullOrEmpty(paginationParam.RowClientId))
            entities = entities.Where(a => a.RowClientId == paginationParam.RowClientId);
        if (paginationParam.StatusCode != null)
            entities = entities.Where(a => a.StatusCode == (int)paginationParam.StatusCode);
        if (paginationParam.TablesName != null && paginationParam.TablesName.Any())
            entities = entities.Where(a => paginationParam.TablesName.Contains(a.TableName));
        if (paginationParam.Levels != null && paginationParam.Levels.Any())
            entities = entities.Where(a => paginationParam.Levels.Contains(a.Level));
        if (paginationParam.States != null && paginationParam.States.Any())
            entities = entities.Where(a => paginationParam.States.Contains(a.State));
        if (paginationParam.IsLogin == true)
            entities = entities.Where(a => a.IsLogin == true);
        if (paginationParam.IsLogout == true)
            entities = entities.Where(a => a.IsLogout == true);
        if (paginationParam.IsShowUser == true)
            entities = entities.Where(a => a.IsShowUser == true);
    }
   
}

