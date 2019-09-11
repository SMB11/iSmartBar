using BusinessEntities.Location;
using Common.DataAccess;
using Facade.Repository;
using iSmartBar.Repositories.LinqToDB;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace iSmartBar.Repositories.Implementation.Location
{
    public class HotelRepository : SimpleRepositoryBase<Hotel, int, IHotelRepository>, IHotelRepository
    {

        public HotelRepository(ISmartBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<Hotel>>> TableExpression => c => ((ISmartBarDB)c).Hotels;

        public List<HotelWithCity> GetHotelWithCities(string languageID)
        {
            var query = Context.QueryProc<HotelWithCity>("GetHotelsWithCityName", new DataParameter("lang", languageID));

            return query.ToList();
        }
    }
}
