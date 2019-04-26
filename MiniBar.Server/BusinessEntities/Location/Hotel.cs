using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

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
}
