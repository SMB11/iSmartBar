using BusinessEntities.Location;
using Facade.Managers;
using Facade.Repository;
using Managers.Base;
using SharedEntities.DTO.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Managers.Implementation
{
    public class CityManager : ManagerBase, ICityManager
    {
        public CityManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<CityDTO>> GetAll(int countryID)
        {
            ICityRepository cityRepository = ServiceProvider.GetService<ICityRepository>();
            List<City> cities = await cityRepository.FindAsync(c => c.CountryID == countryID);
            List<CityDTO> dtos = new List<CityDTO>();
            foreach (City cty in cities)
            {
                CityDTO dto = await GetCityDTO(cty);
                dtos.Add(dto);
            }
            return dtos;
        }

        private async Task<CityDTO> GetCityDTO(City city)
        {

            ICityInfoRepository cityInfoRepository = ServiceProvider.GetService<ICityInfoRepository>();
            ICityRepository CityRepository = ServiceProvider.GetService<ICityRepository>();
            CityInfo info = await cityInfoRepository.FindByIDAsync(city.ID, Culture.Name);
            return new CityDTO()
            {
                ID = city.ID,
                Name = info.Name
            };
        }
    }
}
