using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB.Mapping;

namespace BusinessEntities.Products
{
    [Table("Brands")]
    public class Brand : IDEntityBase<int>
    {
        [Column]
        public string Name { get; set; }
    }
}
