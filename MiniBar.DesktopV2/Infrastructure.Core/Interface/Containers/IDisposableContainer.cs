using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IDisposableContainer
    {
        T Disposable<T>(T obj) where T: IDisposable;
    }
}
