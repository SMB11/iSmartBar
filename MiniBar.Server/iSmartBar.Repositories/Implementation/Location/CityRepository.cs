using BusinessEntities.Location;
using Facade.Repository;
using LinqToDB;
using System;
using System.Linq.Expressions;
using iSmartBar.Repositories.Base;
using iSmartBar.Repositories.LinqToDB;

namespace iSmartBar.Repositories.Implementation.Location
{
    public class HotelRepository : RepositoryBase<Hotel, int, IHotelRepository>, IHotelRepository
    {

        internal override Expression<Func<ISmartBarDB, ITable<Hotel>>> TableExpression => c => c.Hotels;
    }
}
