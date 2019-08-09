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
using Common.Core;
using Common.ResponseHandling;
using System.Globalization;
using Common.Validation;

namespace Managers.Implementation
{
    public class CityManager : ManagerBase, ICityManager
    {
        public CityManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        public async Task<List<CityDTO>> GetAll()
        {
            ICityRepository cityRepository = ServiceProvider.GetService<ICityRepository>();
            List<City> cities = await cityRepository.GetAllAsync();
            return await GetDTOs(cities);
        }

        public async Task<List<CityDTO>> GetByCountryID(int countryID)
        {
            ICityRepository cityRepository = ServiceProvider.GetService<ICityRepository>();
            List<City> cities = await cityRepository.FindAsync(c => c.CountryID == countryID);
            return await GetDTOs(cities);
        }


        public async Task<CityUploadDTO> GetForUplaodByID(int id)
        {
            ICityRepository cityRepository = ServiceProvider.GetService<ICityRepository>();

            City city = await cityRepository.FindByIDAsync(id);
            ICityInfoRepository cityInfoRepository = ServiceProvider.GetService<ICityInfoRepository>();

            List<CityInfo> infos = await cityInfoRepository.FindAsync((inf) => inf.CityID == city.ID);
            Dictionary<string, string> info = new Dictionary<string, string>();
            foreach (var data in infos)
            {
                info.Add(data.LanguageID, data.Name);
            }
            return new CityUploadDTO()
            {
                ID = city.ID,
                Names = info,
                CountryID = city.CountryID
            };
        }


        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task<int> InsertAsync(CityUploadDTO city)
        {
            ICityRepository cityRepository = ServiceProvider.GetService<ICityRepository>();
            ICityInfoRepository cityInfoRepository = ServiceProvider.GetService<ICityInfoRepository>();

            City toInsert = new City() { CountryID = city.CountryID };
            City inserted = await cityRepository.InsertAsync(toInsert);
            
            foreach (CultureInfo culture in LocalizationOptions.Value.SupportedCultures)
            {
                if (!city.Names.ContainsKey(culture.Name))
                {
                    throw new ApiException(Common.Enums.FaultCode.NotAllCulturesProvided, culture.EnglishName + " needs to be filled.");
                }
            }

            List<CityInfo> names = new List<CityInfo>();
            foreach (var pair in city.Names)
            {
                await cityInfoRepository.InsertAsync(new CityInfo()
                {
                    CityID = inserted.ID,
                    LanguageID = pair.Key,
                    Name = pair.Value
                });
            }

            return inserted.ID;
        }


        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task UpdateAsync(CityUploadDTO city)
        {
            IDValidator.AssureID(city.ID);

            ICityRepository cityRepository = ServiceProvider.GetService<ICityRepository>();
            ICityInfoRepository cityInfoRepository = ServiceProvider.GetService<ICityInfoRepository>();

            await cityRepository.UpdateAsync(new City { ID = city.ID, CountryID = city.CountryID });
            List<CityInfo> names = new List<CityInfo>();
            foreach (var pair in city.Names)
            {
                await cityInfoRepository.UpdateAsync(new CityInfo()
                {
                    CityID = city.ID,
                    LanguageID = pair.Key,
                    Name = pair.Value
                });
            }

        }


        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task RemoveAsync(int id)
        {
            IDValidator.AssureID(id);
            ICityRepository cityRepository = ServiceProvider.GetService<ICityRepository>();
            ICityInfoRepository cityInfoRepository = ServiceProvider.GetService<ICityInfoRepository>();

            List<CityInfo> names = await cityInfoRepository.FindAsync(c => c.CityID == id);
            foreach (CityInfo name in names)
            {
                await cityInfoRepository.RemoveAsync(name);
            }
            await cityRepository.RemoveAsync(new City { ID = id });

        }


        private async Task<CityDTO> GetDTO(City city)
        {
            ICityInfoRepository cityInfoRepository = ServiceProvider.GetService<ICityInfoRepository>();
            ICountryInfoRepository countryRepository = ServiceProvider.GetService<ICountryInfoRepository>();
            return new CityDTO()
            {
                ID = city.ID,
                Name = (await cityInfoRepository.FindByIDAsync(city.ID, Culture.Name)).Name,
                CountryID = city.CountryID,
                Country = (await countryRepository.FindByIDAsync(city.CountryID, Culture.Name)).Name
            };
        }

        private async Task<List<CityDTO>> GetDTOs(List<City> cities)
        {
            List<CityDTO> res = new List<CityDTO>();
            foreach (City city in cities)
            {
                res.Add(await GetDTO(city));
            }
            return res;
        }
    }
}
