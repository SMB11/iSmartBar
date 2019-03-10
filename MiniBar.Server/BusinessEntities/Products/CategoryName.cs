using BusinessEntities.Culture;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Products
{
    [Table("CategoryNames")]
    public class CategoryName
    {
        [PrimaryKey]
        public int CategoryID { get; set; }
        [PrimaryKey]
        public string LanguageID { get; set; }
        [Association(ThisKey = nameof(LanguageID), OtherKey = "ID")]
        public Language Language { get; set; }
        [Association(ThisKey = nameof(CategoryID), OtherKey = "ID")]
        public Category Category { get; set; }
        [Column]
        public string Name { get; set; }
    }
}
