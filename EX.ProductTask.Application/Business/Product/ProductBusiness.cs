using Application.Business.Common;
using Application.Common.Pagination;
using Application.IBusiness.Common;
using Application.Services;
using AutoMapper;
using Application.Dtos.Product;
using Application.IBusiness.Products;
using Core.Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.Common;
using Core.Enums;

namespace Application.Business.Products
{
    public class ProductBusiness : EntitiesBusinessCommon<
        Product,
        ProductEditDto,
        ProductEditDto,
        ProductEditDto,
        PaginationParam
        >, IProductBusiness
    {

        private readonly IConfiguration _configuration;
        private readonly IRepositoryApp<Category> _categoryRepo;
        private readonly ILogCustom _logger;

        public ProductBusiness(
            IRepositoryApp<Product> Repo,
            IRepositoryApp<Category> categoryRepo,
            IMapper mapper,
            IHttpContextAccessor accssor,
            IRepositoryMessage IRepositoryMessage,
            IClockService iClockService,
            ILogCustom logger,
            IConfiguration configuration)
            : base(Repo, mapper, accssor, IRepositoryMessage, iClockService)
        {
            _configuration = configuration;
            _categoryRepo = categoryRepo;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductEditDto>> GetAllProducts()
        {
            var repo = await _repo.GetAllAsync(a => a.Category);
            var result = _mapper.Map<IEnumerable<ProductEditDto>>(repo);
            _logger.Info<Product>(MessageReturn.Common_SearchFor, AuditType.get);

            return result;
        }
        public async Task<IEnumerable<CategoryEditDto>> GetAllCategories()
        {
            var repo = await _categoryRepo.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CategoryEditDto>>(repo);
            _logger.Info<Category>(MessageReturn.Common_SearchFor, AuditType.get);

            return result;
        }
        public override async Task Register(ProductEditDto TRegister)
        {
            var entityFound = await _repo.SingleOrDefaultAsync(a => a.Id == TRegister.Id);
            if (entityFound is not null)
                throw new ExceptionCommonReponse(MessageReturn.Common_Found, 400);
            string pathPhysical = _configuration.GetSection("AppSettings:PhysicalPath").Value;
            var entity = _mapper.Map<Product>(TRegister);
            entity.URL = await CreateImageAsync(TRegister.File, pathPhysical);
            LogRowRegister(ref entity);
            _repo.Add(entity);
            _logger.Info<Product>(MessageReturn.Common_SuccessRegister, AuditType.register);

            await _repo.SaveAllAsync();
        }


        public override async Task Edit(int id, ProductEditDto TRegister)
        {
            var existingEntity = await _repo.GetByIdAsync(id);
            if (existingEntity == null)
                throw new ExceptionCommonReponse(MessageReturn.Common_NotFound, 404);
            var oldePath = existingEntity.URL;

            // Ensure proper mapping of properties
            _mapper.Map(TRegister, existingEntity);

            string pathPhysical = _configuration.GetSection("AppSettings:PhysicalPath").Value;

            // Check if a file is being updated
            if (TRegister.File != null)
            {
                // Remove existing file if any
                if (!string.IsNullOrEmpty(existingEntity.URL))
                {
                    RemoveFile(pathPhysical, existingEntity.URL);
                }

                // Create or update URL
                existingEntity.URL = await CreateImageAsync(TRegister.File, pathPhysical);
            } else
            {
                existingEntity.URL = oldePath;

            }
            LogRowEdit(ref existingEntity);
            await _repo.SaveAllAsync();
        }

        public override async Task DeleteById(int id)
        {
            string pathPhysical = _configuration.GetSection("AppSettings:PhysicalPath").Value;
            var entity = await _repo.GetByIdAsync(id) ?? throw new ExceptionCommonReponse(MessageReturn.Common_NotFound, 400);
            _repo.Delete(entity);
            RemoveFile(pathPhysical, entity.URL);
            _logger.Info<Product>(MessageReturn.Common_SuccessDelete, AuditType.delete);

            await _repo.SaveAllAsync();
        }


        private async Task<string> CreateImageAsync(IFormFile file, string pathPhysical, bool isImageOnly = false)
        {
            if (file == null)
                return string.Empty;
            string extension = Path.GetExtension(file.FileName);
            string fileServer = $"{Guid.NewGuid().ToString("N")}{extension}";
            string filePath = Path.Combine(pathPhysical, fileServer).ToLower();
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), pathPhysical)))
                Directory.CreateDirectory(pathPhysical);
            var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            await fileStream.DisposeAsync();
            return $"{fileServer}";
        }


        private void RemoveFile(string FolderName, string FileName)
        {
            var CurrentDirectory = Path.Combine(FolderName, FileName);
            if (File.Exists(CurrentDirectory))
                File.Delete(CurrentDirectory);
        }
    }
}
