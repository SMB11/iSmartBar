using SharedEntities.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedEntities.DTO.Product
{
    public class ProductDTO : ProductLangData
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string Category { get; set; }
        public int BrandID { get; set; }
        public float Price { get; set; }
        public string Brand { get; set; }
        public string ImagePath { get; set; }
        public ProductSize Size { get; set; }
    }

    public class ProductLangData
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    
    public class ProductUploadDTO
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
        public float Price { get; set; }
        public ProductSize Size { get; set; }
        public Dictionary<string, ProductLangData> Info { get; set; }
        public string ImagePath { get; set; }
        public byte[] Image { get; set; }
    }
}
