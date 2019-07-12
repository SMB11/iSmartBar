using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface ISupportsListManipulation
    {
        Task RefreshItems(int? focuseID = null);
        void RemoveFromList(int id);
    }
}
