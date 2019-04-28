using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities.Global;
using LinqToDB.Mapping;

namespace BusinessEntities.Products
{
    [Table("Brands")]
    public class Brand : IDEntityBase<int>
    {
        [Column]
        public string Name { get; set; }
        [Column]
        public int? ImageID { get; set; }

        [Association(ThisKey = nameof(ImageID), OtherKey = "ID")]
        public Image Image { get; set; }
    }
}
