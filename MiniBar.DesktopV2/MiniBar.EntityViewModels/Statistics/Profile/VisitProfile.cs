using AutoMapper;
using SharedEntities.DTO.Statistics;

namespace MiniBar.EntityViewModels.Statistics
{
    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            this.CreateMap<VisitDTO, VisitViewModel>();
        }
    }
}
