using BusinessEntities.Location;
using Common.DataAccess;
using Facade.Repository;
using iSmartBar.Repositories.LinqToDB;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Linq.Expressions;

namespace iSmartBar.Repositories.Implementation.Location
{
    public class CityRepository : SimpleRepositoryBase<City, int, ICityRepository>, ICityRepository
    {
        public CityRepository(ISmartBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<City>>> TableExpression => c => ((ISmartBarDB)c).Cities;
    }
}
