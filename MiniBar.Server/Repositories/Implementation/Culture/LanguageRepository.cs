using BusinessEntities.Culture;
using Facade.Repository;
using LinqToDB;
using Repositories.Base;
using Repositories.LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repositories.Implementation.Culture
{
    public class LanguageRepository : RepositoryBase<Language, string, ILanguageRepository>, ILanguageRepository
    {
        internal override Expression<Func<DBContext, ITable<Language>>> TableExpression => c => c.Languages;
    }
}
