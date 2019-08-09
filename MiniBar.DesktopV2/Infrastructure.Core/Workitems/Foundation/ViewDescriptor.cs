using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Workitems
{
    public class ViewDescriptor
    {
        public ViewDescriptor(object modified, object unmodified)
        {
            Modified = modified;
            Unmodified = unmodified;
        }

        public object Modified { get; set; }
        public object Unmodified { get; set; }
    }
}
