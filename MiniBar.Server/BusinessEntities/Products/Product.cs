using BusinessEntities.Enums;
using LinqToDB.Mapping;

namespace BusinessEntities.Products
{
    [Table("Products")]
    public class Product : IDEntityBase<int>
    {
        [Column]
        public int CategoryID { get; set; }

        [Column]
        public int BrandID { get; set; }

        [Association(ThisKey = nameof(BrandID), OtherKey = "ID")]
        public Brand Brand { get; set; }

        [Column]
        public float Price { get; set; }

        [Column]
        public ProductSize Size { get; set; }
    }
}
