using BusinessEntities.Culture;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface ILanguageRepository : IRepository<Language, string, ILanguageRepository>
    {
    }
}
