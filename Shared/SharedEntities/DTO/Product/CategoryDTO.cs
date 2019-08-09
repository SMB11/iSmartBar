using System;
using System.Collections.Generic;
using System.Text;

namespace SharedEntities.DTO.Product
{
    public class CategoryDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string LanguageID { get; set; }

        public int? ParentID { get; set; }
    }

    public class CategoryUploadDTO
    {
        public int ID { get; set; }

        public int? ParentID { get; set; }

        public IDictionary<string, string> Names { get; set; }
    }
}
