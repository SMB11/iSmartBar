using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;

namespace Infrastructure.Workitems
{
    public abstract class NullWorkitem : WorkitemWpfBase
    {
        public NullWorkitem(IContainerExtension container) : base(container)
        {
        }

        
    }
}
