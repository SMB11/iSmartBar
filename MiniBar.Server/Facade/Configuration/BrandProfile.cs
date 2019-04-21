using AutoMapper;
using BusinessEntities.Products;
using SharedEntities.DTO.Product;

namespace Facade.Configuration
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDTO>();
            CreateMap<BrandDTO, Brand>();
        }
    }
}
