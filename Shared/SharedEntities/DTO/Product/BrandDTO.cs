using System;
using System.Collections.Generic;
using System.Text;

namespace SharedEntities.DTO.Product
{
    public class BrandDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }

    public class BrandUploadDTO
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public byte[] Image { get; set; }
        public bool ImageChanged { get; set; }
    }
    
}
