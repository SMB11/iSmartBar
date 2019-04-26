using BusinessEntities.Location;
using Facade.Repository;
using LinqToDB;
using iSmartBar.Repositories.Base;
using iSmartBar.Repositories.LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iSmartBar.Repositories.Implementation.Location
{
    public class CityInfoRepository : CompositeRepositoryBase<CityInfo, int, string, ICityInfoRepository>, ICityInfoRepository
    {
        internal override Expression<Func<ISmartBarDB, ITable<CityInfo>>> TableExpression => c => c.CityInfos;

        public override CityInfo FindByID(int cityID, string languageID)
        {

            using (ISmartBarDB context = new ISmartBarDB())
            {
                return ExecuteSelect(t => t.Where(e => e.LanguageID == languageID && e.CityID == cityID), context).Single();
            }
        }

        public override async Task<CityInfo> FindByIDAsync(int cityID, string languageID, CancellationToken token = default(CancellationToken))
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                return (await ExecuteSelectAsync(t => t.Where(e => e.LanguageID == languageID && e.CityID == cityID), context)).Single();
            }
        }
    }
}
