using LinqToDB.Mapping;

namespace BusinessEntities.Location
{

    [Table("CityInfo")]
    public class CityInfo
    {
        [Column, PrimaryKey]
        public int CityID { get; set; }
        [Column, PrimaryKey]
        public string LanguageID { get; set; }
        [Column]
        public string Name { get; set; }
    }
}
