using System;
using System.Collections.Generic;
using System.Text;

namespace SharedEntities.DTO.Locations
{
    public class CityDTO
    {
        public int ID { get; set; }

        public int CountryID { get; set; }

        public string Country { get; set; }

        public string Name { get; set; }
    }
    
    public class CityUploadDTO
    {
        public int ID { get; set; }

        public int CountryID { get; set; }

        public Dictionary<string,string> Names { get; set; }
    }
}
