namespace SharedEntities.DTO.Updates
{
    public class AssemblyDTO
    {
        public int Major { get; set; }

        public int Minor { get; set; }

        public int Build { get; set; }

        public int Revision { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string GetVersion()
        {
            return Major + "." + Minor + "." + Build + "." + Revision;
        }
    }
}
