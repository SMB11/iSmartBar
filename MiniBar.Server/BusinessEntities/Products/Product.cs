﻿using BusinessEntities.Enums;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Products
{
    [Table("Products")]
    public class Product : IDEntityBase<int>
    {
        [Column]
        public int CategoryID { get; set; }

        [Column]
        public int BrandID { get; set; }

        [Column]
        public float Price { get; set; }

        [Column]
        public ProductSize Size { get; set; }
    }
}