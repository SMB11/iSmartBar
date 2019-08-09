using Infrastructure.Modularity;
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
        object Register(object view, ScreenRegion region);
        void Unregister(object view, ScreenRegion region);
        void ImportFrom(IViewContainer viewContainer);
    }
}
