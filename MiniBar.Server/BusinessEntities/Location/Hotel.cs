using Common.DataAccess;
using LinqToDB.Mapping;

namespace BusinessEntities.Location
{
    [Table("Hotels")]
    public class Hotel : IDEntityBase<int>
    {

        [Column]
        public int CityID { get; set; }

        [Column]
        public string Name { get; set; }
    }


    public class HotelWithCity
    {
        public int HotelID { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Name { get; set; }
    }
}
