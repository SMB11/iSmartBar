﻿using BusinessEntities.Products;
using Facade.Repository;
using LinqToDB;
using Repositories.Base;
using Repositories.LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Implementation.Products
{
    public class BrandRepository : RepositoryBase<Brand, int, BrandRepository>, IBrandRepository
    {
        internal override Expression<Func<DBContext, ITable<Brand>>> TableExpression => c => c.Brands;
        
    }
}