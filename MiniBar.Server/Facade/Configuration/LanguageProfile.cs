using AutoMapper;
using BusinessEntities.Culture;
using SharedEntities.DTO.Global;

namespace Facade.Configuration
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<Language, LanguageDTO>();
            CreateMap<LanguageDTO, Language>();
        }
    }
}
