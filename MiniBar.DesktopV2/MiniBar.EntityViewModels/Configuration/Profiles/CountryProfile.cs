using AutoMapper;
using SharedEntities.DTO.Locations;

namespace MiniBar.EntityViewModels.Configuration.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            this.CreateMap<CountryDTO, CountryViewModel>();
        }
    }
}
