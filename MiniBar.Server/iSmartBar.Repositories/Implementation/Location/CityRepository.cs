using BusinessEntities.Location;
using Facade.Repository;
using LinqToDB;
using System;
using System.Linq.Expressions;
using iSmartBar.Repositories.Base;
using iSmartBar.Repositories.LinqToDB;
using System.Collections.Generic;

namespace iSmartBar.Repositories.Implementation.Location
{
    public class CityRepository : RepositoryBase<City, int, ICityRepository>, ICityRepository
    {

        internal override Expression<Func<ISmartBarDB, ITable<City>>> TableExpression => c => c.Cities;

    }
}
