using System;
using System.Collections.Generic;
using System.Text;

namespace SharedEntities.DTO.Locations
{
    public class CountryDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }


    public class CountryUploadDTO
    {
        public int ID { get; set; }
        
        public Dictionary<string, string> Names { get; set; }
    }
}
