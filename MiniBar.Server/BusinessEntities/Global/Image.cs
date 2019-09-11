using Common.DataAccess;
using LinqToDB.Mapping;

namespace BusinessEntities.Global
{
    [Table("Images")]
    public class Image : IDEntityBase<int>
    {
        [Column]
        public string Path { get; set; }
    }
}
