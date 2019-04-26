using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

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
