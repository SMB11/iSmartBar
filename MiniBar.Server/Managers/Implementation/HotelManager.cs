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

namespace Managers.Implementation
{
    public class HotelManager : ManagerBase, IHotelManager
    {
        public HotelManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<HotelDTO>> GetAll(int cityID)
        {
            IHotelRepository hotelRepository = ServiceProvider.GetService<IHotelRepository>();
            List<Hotel> hotels = await hotelRepository.FindAsync(c => c.CityID == cityID);
            return hotels.Select(h => new HotelDTO { ID = h.ID, Name = h.Name }).ToList();
        }
    }
    }
