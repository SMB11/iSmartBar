using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Interface
{
    public interface IViewContainer : IDisposable
    {
        TView Register<TView>(object obj);
        object Register(object obj);
    }
}
