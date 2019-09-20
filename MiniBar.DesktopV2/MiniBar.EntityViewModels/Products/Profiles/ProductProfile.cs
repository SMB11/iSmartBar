using AutoMapper;
using SharedEntities.DTO.Product;

namespace MiniBar.EntityViewModels.Products.Profiles
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {
            this.CreateMap<ProductDTO, ProductViewModel>();
            this.CreateMap<ProductViewModel, ProductViewModel>();
        }
    }
}
