using BusinessEntities.Location;
using Facade.Repository;
using LinqToDB;
using System;
using System.Linq.Expressions;
using iSmartBar.Repositories.Base;
using iSmartBar.Repositories.LinqToDB;
using System.Collections.Generic;
using LinqToDB.Data;
using System.Linq;

namespace iSmartBar.Repositories.Implementation.Location
{
    public class HotelRepository : RepositoryBase<Hotel, int, IHotelRepository>, IHotelRepository
    {

        internal override Expression<Func<ISmartBarDB, ITable<Hotel>>> TableExpression => c => c.Hotels;

        public List<HotelWithCity> GetHotelWithCities(string languageID)
        {
            using(ISmartBarDB db = new ISmartBarDB())
            {

                var query = db.QueryProc<HotelWithCity>("GetHotelsWithCityName", new DataParameter("lang", languageID));

                return query.ToList();
            }
        }
    }
}
