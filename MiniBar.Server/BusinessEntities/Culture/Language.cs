using LinqToDB.Mapping;

namespace BusinessEntities.Culture
{
    [Table("Languages")]
    public class Language : IDEntityBase<string>
    {
        [Column]
        public string Name { get; set; }
    }
}
