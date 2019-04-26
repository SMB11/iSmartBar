using BusinessEntities.Location;
using Facade.Repository;
using LinqToDB;
using System;
using System.Linq.Expressions;
using iSmartBar.Repositories.Base;
using iSmartBar.Repositories.LinqToDB;

namespace iSmartBar.Repositories.Implementation.Location
{
    public class CountryRepository : RepositoryBase<Country, int, ICountryRepository>, ICountryRepository
    {

        internal override Expression<Func<ISmartBarDB, ITable<Country>>> TableExpression => c => c.Countries;
    }
}
