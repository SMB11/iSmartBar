using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Base
{
    public interface IIdEntityBase<T>
    {
        T ID { get; set; }
    }
}
