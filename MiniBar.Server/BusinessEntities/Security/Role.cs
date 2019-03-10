using LinqToDB.Identity;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Security
{
    [Table("IdentityRole")]
    public class Role : IdentityRole
    {

        public Role() {}
        public Role(string name) : base(name) { }
    }
}
