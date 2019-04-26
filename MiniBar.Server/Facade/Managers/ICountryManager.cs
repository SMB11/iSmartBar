using SharedEntities.DTO.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface ICountryManager
    {
        Task<List<CountryDTO>> GetAll();
    }
}
