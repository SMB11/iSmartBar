using Prism.Ioc;

namespace Infrastructure.Workitems
{
    public abstract class Workitem : WorkitemWpfBase
    {

        public Workitem(IContainerExtension container) : base(container)
        {
        }

    }
}
