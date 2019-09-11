using BusinessEntities.Statistics;
using Common.DataAccess;
using Facade.Repository;
using iSmartBar.Repositories.LinqToDB;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Linq.Expressions;

namespace iSmartBar.Repositories.Implementation.Statistics
{
    public class VisitRepository : SimpleRepositoryBase<Visit, int, IVisitRepository>, IVisitRepository
    {
        public VisitRepository(ISmartBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<Visit>>> TableExpression => c => ((ISmartBarDB)c).Visits;

    }
}
