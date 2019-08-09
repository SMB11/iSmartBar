using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IShell
    {

        void ShowLoading(string action);
        void EndLoading();
    }
}
