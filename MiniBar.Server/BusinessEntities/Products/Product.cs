﻿using BusinessEntities.Enums;
using BusinessEntities.Global;
using Common.DataAccess;
using LinqToDB.Mapping;

namespace BusinessEntities.Products
{
    [Table("Products")]
    public class Product : IDEntityBase<int>
    {
        [Column]
        public int CategoryID { get; set; }


        [Association(ThisKey = nameof(CategoryID), OtherKey = "ID")]
        public Category Category { get; set; }

        [Column]
        public int BrandID { get; set; }

        [Association(ThisKey = nameof(BrandID), OtherKey = "ID")]
        public Brand Brand { get; set; }

        [Column]
        public float Price { get; set; }

        [Column]
        public ProductSize Size { get; set; }

        [Column]
        public int? ImageID { get; set; }

        [Association(ThisKey = nameof(ImageID), OtherKey = "ID")]
        public Image Image { get; set; }
    }
}
