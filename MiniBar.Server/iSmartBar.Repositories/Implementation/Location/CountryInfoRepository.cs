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
    public class CountryInfoRepository : CompositeRepositoryBase<CountryInfo, int, string, ICountryInfoRepository>, ICountryInfoRepository
    {
        internal override Expression<Func<ISmartBarDB, ITable<CountryInfo>>> TableExpression => c => c.CountryInfos;

        public override CountryInfo FindByID(int countryID, string languageID)
        {

            using (ISmartBarDB context = new ISmartBarDB())
            {
                return ExecuteSelect(t => t.Where(e => e.LanguageID == languageID && e.CountryID == countryID), context).Single();
            }
        }

        public override async Task<CountryInfo> FindByIDAsync(int countryID, string languageID, CancellationToken token = default(CancellationToken))
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                return (await ExecuteSelectAsync(t => t.Where(e => e.LanguageID == languageID && e.CountryID == countryID), context)).Single();
            }
        }
    }
}
