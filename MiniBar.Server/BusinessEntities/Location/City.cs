using Common.DataAccess;
using LinqToDB.Mapping;

namespace BusinessEntities.Location
{
    [Table("Cities")]
    public class City : IDEntityBase<int>
    {
        [Column]
        public int CountryID { get; set; }

        [Association(ThisKey = nameof(CountryID), OtherKey = "ID")]
        public Country Country { get; set; }
    }
}
