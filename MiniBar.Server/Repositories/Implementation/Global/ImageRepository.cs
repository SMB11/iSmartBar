using BusinessEntities.Global;
using Common.DataAccess;
using Facade.Repository;
using LinqToDB;
using LinqToDB.Data;
using Repositories.LinqToDB;
using System;
using System.Linq.Expressions;

namespace Core.Repositories.Implementation
{
    public class ImageRepository : SimpleRepositoryBase<Image, int, IImageRepository>, IImageRepository
    {
        public ImageRepository(MiniBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<Image>>> TableExpression => c => ((MiniBarDB)c).Images;

    }
}
