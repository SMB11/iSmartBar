using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MVVM
{
    public class BaseViewModel : ViewModelBase, IDisposable
    {
        public virtual void Dispose()
        {
        }
    }
}
