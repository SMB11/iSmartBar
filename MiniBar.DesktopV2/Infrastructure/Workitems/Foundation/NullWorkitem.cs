using Prism.Ioc;

namespace Infrastructure.Workitems
{
    public abstract class NullWorkitem : WorkitemWpfBase
    {
        public NullWorkitem(IContainerExtension container) : base(container)
        {
        }


    }
}
