using AutoMapper;
using SharedEntities.DTO.Product;

namespace MiniBar.EntityViewModels.Products.Profiles
{
    public class CategoryProfile : Profile
    {

        public CategoryProfile()
        {
            this.CreateMap<CategoryDTO, CategoryViewModel>();
        }
    }
}
