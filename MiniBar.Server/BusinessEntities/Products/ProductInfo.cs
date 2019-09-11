using LinqToDB.Mapping;

namespace BusinessEntities.Products
{
    [Table("ProductInfo")]
    public class ProductInfo
    {
        [Column, PrimaryKey]
        public int ProductID { get; set; }
        [Column, PrimaryKey]
        public string LanguageID { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
    }
}
