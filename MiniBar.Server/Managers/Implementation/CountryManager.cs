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
using Common.Core;
using System.Globalization;
using Common.ResponseHandling;
using Common.Validation;

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
            return await GetDTOs(countries);
        }



        public async Task<CountryUploadDTO> GetForUplaodByID(int id)
        {
            ICountryRepository countryRepository = ServiceProvider.GetService<ICountryRepository>();

            Country country = await countryRepository.FindByIDAsync(id);
            ICountryInfoRepository countryInfoRepository = ServiceProvider.GetService<ICountryInfoRepository>();

            List<CountryInfo> infos = await countryInfoRepository.FindAsync((inf) => inf.CountryID == country.ID);
            Dictionary<string, string> info = new Dictionary<string, string>();
            foreach (var data in infos)
            {
                info.Add(data.LanguageID, data.Name);
            }
            return new CountryUploadDTO()
            {
                ID = country.ID,
                Names = info
            };
        }


        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task<int> InsertAsync(CountryUploadDTO country)
        {
            ICountryRepository countryRepository = ServiceProvider.GetService<ICountryRepository>();
            ICountryInfoRepository countryInfoRepository = ServiceProvider.GetService<ICountryInfoRepository>();
            Country toInsert = new Country();
            Country inserted = await countryRepository.InsertAsync(toInsert);
            CountryInfo countryInfo = new CountryInfo() {
                CountryID = inserted.ID,
                
            };
            foreach (CultureInfo culture in LocalizationOptions.Value.SupportedCultures)
            {
                if (!country.Names.ContainsKey(culture.Name))
                {
                    throw new ApiException(Common.Enums.FaultCode.NotAllCulturesProvided, culture.EnglishName + " needs to be filled.");
                }
            }

            List<CountryInfo> names = new List<CountryInfo>();
            foreach (var pair in country.Names)
            {
                await countryInfoRepository.InsertAsync(new CountryInfo()
                {
                    CountryID = inserted.ID,
                    LanguageID = pair.Key,
                    Name = pair.Value
                });
            }

            return inserted.ID;
        }


        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task UpdateAsync(CountryUploadDTO country)
        {
            IDValidator.AssureID(country.ID);
            ICountryInfoRepository countryInfoRepository = ServiceProvider.GetService<ICountryInfoRepository>();
            ICountryRepository countryRepository = ServiceProvider.GetService<ICountryRepository>();
            
            List<CountryInfo> names = new List<CountryInfo>();
            foreach (var pair in country.Names)
            {
                await countryInfoRepository.UpdateAsync(new CountryInfo()
                {
                    CountryID = country.ID,
                    LanguageID = pair.Key,
                    Name = pair.Value
                });
            }

        }


        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task RemoveAsync(int id)
        {
            IDValidator.AssureID(id);
            ICountryInfoRepository countryInfoRepository = ServiceProvider.GetService<ICountryInfoRepository>();
            ICountryRepository countryRepository = ServiceProvider.GetService<ICountryRepository>();

            List<CountryInfo> names = await countryInfoRepository.FindAsync(c => c.CountryID == id);
            foreach (CountryInfo name in names)
            {
                await countryInfoRepository.RemoveAsync(name);
            }
            await countryRepository.RemoveAsync(new Country { ID = id });

        }


        private async Task<CountryDTO> GetDTO(Country country)
        {
            ICountryInfoRepository countryInfoRepository = ServiceProvider.GetService<ICountryInfoRepository>();
            return new CountryDTO()
            {
                ID = country.ID,
                Name = (await countryInfoRepository.FindByIDAsync(country.ID, Culture.Name)).Name
            };
        }

        private async Task<List<CountryDTO>> GetDTOs(List<Country> countries)
        {
            List<CountryDTO> res = new List<CountryDTO>();
            foreach (Country country in countries)
            {
                res.Add(await GetDTO(country));
            }
            return res;
        }
    }
}
