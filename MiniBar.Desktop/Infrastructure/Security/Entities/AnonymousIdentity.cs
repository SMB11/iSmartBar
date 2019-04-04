using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Entities
{
    public class AnonymousIdentity : AppIdentity
    {
        public AnonymousIdentity() : base(null, null)
        {
        }
    }
}
