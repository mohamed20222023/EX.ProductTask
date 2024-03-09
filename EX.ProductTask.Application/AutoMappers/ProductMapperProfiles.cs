using Application.Dtos.Product;
using AutoMapper;
using Core.Entities.Products;

namespace Application.AutoMappers
{
    public class ProductMapperProfiles : Profile
    {
        public ProductMapperProfiles()
        {
            CreateMap<ProductEditDto, Product>()
            .ForMember(d => d.URL, op => op.MapFrom(t => t.URL));
			CreateMap<Product, ProductEditDto>().
            ForMember(d => d.URL, op => op.MapFrom(t => t.URL))
			.ForMember(d => d.UserCreated, op => op.MapFrom(t => t.UserCreatedName))
			.ForMember(d => d.CategoryName, op => op.MapFrom(t => t.Category.Name));


			CreateMap<CategoryEditDto, Category>();
            CreateMap<Category, CategoryEditDto>();
        }
    }
}
