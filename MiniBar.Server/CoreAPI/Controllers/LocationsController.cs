using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedEntities.DTO.Locations;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        [HttpGet("countries")]
        public List<CountryDTO> GetCountries()
        {
            return new List<CountryDTO> {
                new CountryDTO{ID = 1, Name = "America"},
                new CountryDTO{ID = 2, Name = "Italia"},
            };
        }

        [HttpGet("cities/{id}")]
        public List<CityDTO> GetCities(int id)
        {
            return new List<CityDTO> {
                new CityDTO{ID = 1, Name = "City 1"},
                new CityDTO{ID = 2, Name = "City 2"},
            };
        }


        [HttpGet("hotels/{id}")]
        public List<HotelDTO> GetHotels(int id)
        {
            return new List<HotelDTO> {
                new HotelDTO{ID = 1, Name = "Hotel 1"},
                new HotelDTO{ID = 2, Name = "Hotel 2"},
            };
        }
    }
}