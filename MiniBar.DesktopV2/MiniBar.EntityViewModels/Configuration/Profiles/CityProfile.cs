using AutoMapper;
using SharedEntities.DTO.Locations;

namespace MiniBar.EntityViewModels.Configuration.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            this.CreateMap<CityDTO, CityViewModel>();
        }
    }
}
