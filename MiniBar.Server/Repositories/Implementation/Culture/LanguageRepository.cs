using BusinessEntities.Culture;
using Common.DataAccess;
using Facade.Repository;
using LinqToDB;
using LinqToDB.Data;
using Repositories.LinqToDB;
using System;
using System.Linq.Expressions;

namespace Repositories.Implementation.Culture
{
    public class LanguageRepository : SimpleRepositoryBase<Language, string, ILanguageRepository>, ILanguageRepository
    {
        public LanguageRepository(MiniBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<Language>>> TableExpression => c => ((MiniBarDB)c).Languages;
    }
}
