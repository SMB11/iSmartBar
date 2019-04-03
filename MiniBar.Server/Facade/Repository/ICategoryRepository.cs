﻿using BusinessEntities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Repository
{
    public interface ICategoryRepository : IRepository<Category, int, ICategoryRepository>
    {
    }
}
