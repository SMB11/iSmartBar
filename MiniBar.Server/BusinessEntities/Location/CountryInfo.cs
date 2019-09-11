using LinqToDB.Mapping;

namespace BusinessEntities.Location
{

    [Table("CountryInfo")]
    public class CountryInfo
    {
        [Column, PrimaryKey]
        public int CountryID { get; set; }
        [Column, PrimaryKey]
        public string LanguageID { get; set; }
        [Column]
        public string Name { get; set; }
    }
}
