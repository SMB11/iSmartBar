using Facade.Managers;
using Facade.Repository;
using Managers.Base;
using SharedEntities.DTO.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BusinessEntities.Location;

namespace Managers.Implementation
{
    public class CountryManager : ManagerBase, ICountryManager
    {
        public CountryManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<CountryDTO>> GetAll()
        {
            ICountryRepository countryRepository = ServiceProvider.GetService<ICountryRepository>();
            List<Country> countries  = await countryRepository.GetAllAsync();
            List<CountryDTO> dtos = new List<CountryDTO>();
            foreach(Country cty in countries)
            {
                CountryDTO dto = await GetCountryDTO(cty);
                dtos.Add(dto);
            }
            return dtos;
        }

        private async Task<CountryDTO> GetCountryDTO(Country country)
        {

            ICountryInfoRepository countryInfoRepository = ServiceProvider.GetService<ICountryInfoRepository>();
            ICountryRepository countryRepository = ServiceProvider.GetService<ICountryRepository>();
            CountryInfo info = await countryInfoRepository.FindByIDAsync(country.ID, Culture.Name);
            return new CountryDTO()
            {
                ID = country.ID,
                Name = info.Name
            };
        }
    }
}
