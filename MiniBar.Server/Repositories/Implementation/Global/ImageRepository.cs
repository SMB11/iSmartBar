using BusinessEntities.Global;
using Facade.Repository;
using LinqToDB;
using Repositories.Base;
using Repositories.LinqToDB;
using System;
using System.Linq.Expressions;

namespace Core.Repositories.Implementation
{
    public class ImageRepository : RepositoryBase<Image, int, IImageRepository>, IImageRepository
    {
        internal override Expression<Func<MiniBarDB, ITable<Image>>> TableExpression => c => c.Images;
        
    }
}
