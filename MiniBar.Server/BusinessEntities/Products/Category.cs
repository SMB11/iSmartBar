using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Products
{
    [Table("Categories")]
    public class Category : IDEntityBase<int>
    {
        [Column]
        public int? ParentID { get; set; }
    }
}
