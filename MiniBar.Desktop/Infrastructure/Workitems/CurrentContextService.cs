using DXInfrastructure;
using Infrastructure.Constants;
using Infrastructure.Interface;
using Prism.Commands;
using Prism.Ioc;

namespace Infrastructure.Workitems
{
    public class CurrentContextService : ICurrentContextService
    {
        private IContainerExtension Container { get; set; }
        public CurrentContextService(IContainerExtension container)
        {
            Container = container;
            BarCommandManager.RegisterCommand(CommandNames.CloseWorkItem, new DelegateCommand(() => CloseCurrentWorkItem()));
        }

        private IWorkItem _currentWorkItem;

        public bool CloseCurrentWorkItem()
        {
            if (_currentWorkItem == null) return true;
            if ( _currentWorkItem.CanClose())
            {
                _currentWorkItem?.Cleanup();
                _currentWorkItem = null;
                return true;
            }
            return false;
        }

        public void LaunchWorkItem<T>() where T : IWorkItem
        {
            if (_currentWorkItem is T) return;
            if (CloseCurrentWorkItem())
            {
                IWorkItem workitem = Container.Resolve<T>();
                _currentWorkItem = workitem;
                workitem.Run();

            }
        }
    }
}
