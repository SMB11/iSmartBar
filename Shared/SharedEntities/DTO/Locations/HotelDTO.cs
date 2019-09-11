namespace SharedEntities.DTO.Locations
{
    public class HotelDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CityID { get; set; }

    }


    public class HotelDetailedDTO
    {
        public int HotelID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
    }
}
