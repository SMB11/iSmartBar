using AutoMapper;
using MiniBar.EntityViewModels.Configuration;
using SharedEntities.DTO.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBar.EntityViewModels.Configuration.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            this.CreateMap<HotelDetailedDTO, HotelViewModel>().ForMember("ID", e => e.MapFrom("HotelID"));
        }
    }
}
