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
    public class CountryInfoRepository : CompositeRepositoryBase<CountryInfo, int, string, ICountryInfoRepository>, ICountryInfoRepository
    {
        public CountryInfoRepository(ISmartBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<CountryInfo>>> TableExpression => c => ((ISmartBarDB)c).CountryInfos;

        public override CountryInfo FindByID(int countryID, string languageID)
        {
            return ExecuteSelect(t => t.Where(e => e.LanguageID == languageID && e.CountryID == countryID), Context).Single();
        }

        public override async Task<CountryInfo> FindByIDAsync(int countryID, string languageID, CancellationToken token = default(CancellationToken))
        {
            return (await ExecuteSelectAsync(t => t.Where(e => e.LanguageID == languageID && e.CountryID == countryID), Context)).Single();
        }
    }
}
