using Infrastructure.Interface;
using Prism.Ioc;
using Prism.Regions;
using System.Windows;

namespace Infrastructure.Workitems
{
    public abstract class Workitem : WorkitemWpfBase
    {

        public Workitem(IContainerExtension container) : base(container)
        {
        }
        
    }
}
