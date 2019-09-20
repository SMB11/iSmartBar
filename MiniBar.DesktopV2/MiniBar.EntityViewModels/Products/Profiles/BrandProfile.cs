using AutoMapper;
using SharedEntities.DTO.Product;

namespace MiniBar.EntityViewModels.Products.Profiles
{
    public class BrandProfile : Profile
    {

        public BrandProfile()
        {
            this.CreateMap<BrandDTO, BrandViewModel>();
        }
    }
}
