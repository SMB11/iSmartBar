using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Products
{
    [Table("ProductInfo")]
    public class ProductInfo
    {
        [Column]
        public int ProductID { get; set; }
        [Column]
        public string LanguageID { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
    }
}
