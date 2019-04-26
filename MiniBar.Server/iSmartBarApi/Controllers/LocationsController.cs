using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Facade.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using SharedEntities.DTO.Locations;

namespace iSmartBarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ApiControllerBase
    {
        public LocationsController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet("countries")]
        public async Task<List<CountryDTO>> GetCountries()
        {
            ICountryManager countryManager = ServiceProvider.GetService<ICountryManager>();
            return await countryManager.GetAll();
            //return new List<CountryDTO> {
            //    new CountryDTO{ID = 1, Name = "America"},
            //    new CountryDTO{ID = 2, Name = "Italia"},
            //};
        }

        [HttpGet("cities/{id}")]
        public async Task<List<CityDTO>> GetCities(int id)
        {
            ICityManager cityManager = ServiceProvider.GetService<ICityManager>();
            return await cityManager.GetAll(id);
        }


        [HttpGet("hotels/{id}")]
        public async Task<List<HotelDTO>> GetHotels(int id)
        {

            IHotelManager hotelManager = ServiceProvider.GetService<IHotelManager>();
            return await hotelManager.GetAll(id);
        }
    }
}