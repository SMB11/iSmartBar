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

        #region Countries

        [HttpGet("countries")]
        public async Task<List<CountryDTO>> GetCountries()
        {
            ICountryManager countryManager = ServiceProvider.GetService<ICountryManager>();
            return await countryManager.GetAll();
        }


        [HttpGet("countries/forupload/{id}")]
        public async Task<CountryUploadDTO> GetCountryForUploadByID(int id)
        {
            ICountryManager countryManager = ServiceProvider.GetService<ICountryManager>();
            return await countryManager.GetForUplaodByID(id);
        }


        [HttpPost("countries/insert")]
        public async Task<int> InsertCountry(CountryUploadDTO country)
        {
            return await this.ServiceProvider.GetService<ICountryManager>().InsertAsync(country);
        }

        [HttpPost("countries/update")]
        public async Task UpdateCountry(CountryUploadDTO country)
        {
            await this.ServiceProvider.GetService<ICountryManager>().UpdateAsync(country);
        }

        [HttpDelete("countries/{id}")]
        public async Task RemoveCountry(int id)
        {
            await this.ServiceProvider.GetService<ICountryManager>().RemoveAsync(id);
        }

        #endregion

        #region Cities
        [HttpGet("cities")]
        public async Task<List<CityDTO>> GetAllCities()
        {
            ICityManager cityManager = ServiceProvider.GetService<ICityManager>();
            return await cityManager.GetAll();
        }

        [HttpGet("cities/{id}")]
        public async Task<List<CityDTO>> GetCities(int id)
        {
            ICityManager cityManager = ServiceProvider.GetService<ICityManager>();
            return await cityManager.GetByCountryID(id);
        }


        [HttpGet("cities/forupload/{id}")]
        public async Task<CityUploadDTO> GetCityForUploadByID(int id)
        {
            ICityManager cityManager = ServiceProvider.GetService<ICityManager>();
            return await cityManager.GetForUplaodByID(id);
        }


        [HttpPost("cities/insert")]
        public async Task<int> InsertCity(CityUploadDTO country)
        {
            return await this.ServiceProvider.GetService<ICityManager>().InsertAsync(country);
        }

        [HttpPost("cities/update")]
        public async Task UpdateCity(CityUploadDTO country)
        {
            await this.ServiceProvider.GetService<ICityManager>().UpdateAsync(country);
        }

        [HttpDelete("cities/{id}")]
        public async Task RemoveCity(int id)
        {
            await this.ServiceProvider.GetService<ICityManager>().RemoveAsync(id);
        }

        #endregion

        #region Hotels

        [HttpGet("hotels/{id}")]
        public async Task<List<HotelDTO>> GetHotels(int id)
        {
            IHotelManager hotelManager = ServiceProvider.GetService<IHotelManager>();
            return await hotelManager.GetByCityID(id);
        }

        [HttpGet("hotels")]
        public List<HotelDetailedDTO> GetAllHotels()
        {
            IHotelManager hotelManager = ServiceProvider.GetService<IHotelManager>();
            return hotelManager.GetAll();
        }

        [HttpGet("hotels/byid/{id}")]
        public async Task<HotelDTO> GetHotelByID(int id)
        {
            IHotelManager hotelManager = ServiceProvider.GetService<IHotelManager>();
            return await hotelManager.GetByID(id);
        }


        [HttpPost("hotels/insert")]
        public async Task<int> InsertHotel(HotelDTO hotel)
        {
            return await this.ServiceProvider.GetService<IHotelManager>().InsertAsync(hotel);
        }


        [HttpPost("hotels/insertlist")]
        public async Task InsertListHotel(List<HotelDTO> hotels)
        {
            await this.ServiceProvider.GetService<IHotelManager>().InsertMultipleAsync(hotels);
        }

        [HttpPost("hotels/update")]
        public async Task UpdateCity(HotelDTO hotel)
        {
            await this.ServiceProvider.GetService<IHotelManager>().UpdateAsync(hotel);
        }

        [HttpDelete("hotels/{id}")]
        public async Task RemoveHotel(int id)
        {
            await this.ServiceProvider.GetService<IHotelManager>().RemoveAsync(id);
        }

        #endregion
    }
}