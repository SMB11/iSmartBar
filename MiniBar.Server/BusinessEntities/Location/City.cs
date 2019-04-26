using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Location
{
    [Table("Cities")]
    public class City : IDEntityBase<int>
    {
        [Column]
        public int CountryID { get; set; }
    }
}
