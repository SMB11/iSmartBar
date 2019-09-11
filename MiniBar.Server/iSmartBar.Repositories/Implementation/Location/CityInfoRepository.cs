using BusinessEntities.Location;
using Common.DataAccess;
using Facade.Repository;
using iSmartBar.Repositories.LinqToDB;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace iSmartBar.Repositories.Implementation.Location
{
    public class CityInfoRepository : CompositeRepositoryBase<CityInfo, int, string, ICityInfoRepository>, ICityInfoRepository
    {
        public CityInfoRepository(ISmartBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<CityInfo>>> TableExpression => c => ((ISmartBarDB)c).CityInfos;

        public override CityInfo FindByID(int cityID, string languageID)
        {
            return ExecuteSelect(t => t.Where(e => e.LanguageID == languageID && e.CityID == cityID), Context).Single();
        }

        public override async Task<CityInfo> FindByIDAsync(int cityID, string languageID, CancellationToken token = default(CancellationToken))
        {
            return (await ExecuteSelectAsync(t => t.Where(e => e.LanguageID == languageID && e.CityID == cityID), Context)).Single();
        }
    }
}
