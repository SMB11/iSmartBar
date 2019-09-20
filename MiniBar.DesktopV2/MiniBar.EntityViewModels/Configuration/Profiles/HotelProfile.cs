using AutoMapper;
using SharedEntities.DTO.Locations;

namespace MiniBar.EntityViewModels.Configuration.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            this.CreateMap<HotelDetailedDTO, HotelViewModel>().ForMember("ID", e => e.MapFrom("HotelID"));
            this.CreateMap<HotelDTO, HotelViewModel>();
        }
    }
}
