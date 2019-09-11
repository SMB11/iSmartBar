using Common.DataAccess;
using LinqToDB.Mapping;

namespace BusinessEntities.Products
{
    [Table("Categories")]
    public class Category : IDEntityBase<int>
    {
        [Column]
        public int? ParentID { get; set; }
    }
}
