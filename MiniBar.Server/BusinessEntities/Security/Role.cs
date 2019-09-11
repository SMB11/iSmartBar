using LinqToDB.Identity;
using LinqToDB.Mapping;

namespace BusinessEntities.Security
{
    [Table("IdentityRole")]
    public class Role : IdentityRole
    {

        public Role() { }
        public Role(string name) : base(name) { }
    }
}
