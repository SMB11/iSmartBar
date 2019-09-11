using BusinessEntities.Location;
using Common.DataAccess;
using LinqToDB.Mapping;
using System;

namespace BusinessEntities.Statistics
{

    [Table("Visits")]
    public class Visit : IDEntityBase<int>
    {


        [Association(ThisKey = nameof(HotelID), OtherKey = "ID")]
        public Hotel Hotel { get; set; }

        [Column]
        public int HotelID { get; set; }

        [Column]
        public string LanguageID { get; set; }

        [Column("CheckInDate")]
        public DateTime StartDate { get; set; }

        [Column("CheckOutDate")]
        public DateTime EndDate { get; set; }
    }

    public class VisitExtended
    {
        public HotelWithCity Hotel { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
