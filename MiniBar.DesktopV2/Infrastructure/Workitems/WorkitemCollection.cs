using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Workitems
{
    internal class WorkitemCollection : ObservableCollection<IWorkItem>
    {
        public IWorkItem Get(string id)
        {
            foreach(var w in this)
            {
                if (w.WorkItemID == id)
                    return w;
            }
            return null;
        }
    }
}
