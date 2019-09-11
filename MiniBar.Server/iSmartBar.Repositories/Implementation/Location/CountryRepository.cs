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
    public class CountryRepository : SimpleRepositoryBase<Country, int, ICountryRepository>, ICountryRepository
    {

        public CountryRepository(ISmartBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<Country>>> TableExpression => c => ((ISmartBarDB)c).Countries;

    }
}
