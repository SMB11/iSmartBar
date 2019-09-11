using SharedEntities.DTO.Locations;
using System;

namespace SharedEntities.DTO.Statistics
{
    public class VisitDTO
    {

        public int ID { get; set; }

        public HotelDTO Hotel { get; set; }

        public int HotelID { get; set; }

        public string LanguageID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
