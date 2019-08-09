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
using System.Linq;
using Common.Core;
using Common.Validation;
using AutoMapper;

namespace Managers.Implementation
{
    public class HotelManager : ManagerBase, IHotelManager
    {
        public HotelManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<HotelDTO>> GetByCityID(int cityID)
        {
            IHotelRepository hotelRepository = ServiceProvider.GetService<IHotelRepository>();
            List<Hotel> hotels = await hotelRepository.FindAsync(c => c.CityID == cityID);
            return GetDTOs(hotels);
        }


        public List<HotelDetailedDTO> GetAll()
        {
            IHotelRepository HotelRepository = ServiceProvider.GetService<IHotelRepository>();
            List<HotelDetailedDTO> cities = Mapper.Map<List<HotelDetailedDTO>> (HotelRepository.GetHotelWithCities(Culture.Name));
            return cities;
        }

        public async Task<HotelDTO> GetByID(int id)
        {
            IHotelRepository HotelRepository = ServiceProvider.GetService<IHotelRepository>();

            Hotel Hotel = await HotelRepository.FindByIDAsync(id);
            return GetDTO(Hotel);
        }


        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task<int> InsertAsync(HotelDTO Hotel)
        {
            IHotelRepository HotelRepository = ServiceProvider.GetService<IHotelRepository>();

            Hotel toInsert = new Hotel() {  Name = Hotel.Name, CityID = Hotel.CityID };
            Hotel inserted = await HotelRepository.InsertAsync(toInsert);
            
            return inserted.ID;
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task InsertMultipleAsync(List<HotelDTO> hotels)
        {
            foreach (var hotel in hotels)
                await InsertAsync(hotel);
        }


        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task UpdateAsync(HotelDTO Hotel)
        {
            IDValidator.AssureID(Hotel.ID);

            IHotelRepository HotelRepository = ServiceProvider.GetService<IHotelRepository>();

            await HotelRepository.UpdateAsync(new Hotel { ID = Hotel.ID, Name = Hotel.Name, CityID = Hotel.CityID });
            

        }


        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task RemoveAsync(int id)
        {
            IDValidator.AssureID(id);
            IHotelRepository HotelRepository = ServiceProvider.GetService<IHotelRepository>();

            await HotelRepository.RemoveAsync(new Hotel { ID = id });

        }


        private HotelDTO GetDTO(Hotel Hotel)
        {
            ICityInfoRepository cityInfoRepository = ServiceProvider.GetService<ICityInfoRepository>();
            return new HotelDTO()
            {
                ID = Hotel.ID,
                Name = Hotel.Name,
                CityID = Hotel.CityID
            };
        }

        private List<HotelDTO> GetDTOs(List<Hotel> hotels)
        {
            List<HotelDTO> res = new List<HotelDTO>();
            foreach (Hotel Hotel in hotels)
            {
                res.Add(GetDTO(Hotel));
            }
            return res;
        }
    }
}
