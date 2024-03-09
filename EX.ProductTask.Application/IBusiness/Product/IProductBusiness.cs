using Application.Common.Pagination;
using Application.IBusiness.Common;
using Application.Dtos.Product;
using Core.Entities.Products;
using Core.Common;

namespace Application.IBusiness.Products
{
    public interface IProductBusiness : IEntitiesBusinessCommon<
        Product,
        ProductEditDto,
        ProductEditDto,
        ProductEditDto,
        PaginationParam
        >
    {

        Task<IEnumerable<CategoryEditDto>> GetAllCategories();
        Task<IEnumerable<ProductEditDto>> GetAllProducts();

	}
}
