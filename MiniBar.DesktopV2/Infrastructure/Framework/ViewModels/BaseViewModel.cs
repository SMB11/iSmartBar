using DevExpress.Mvvm;
using Infrastructure.Logging;
using Infrastructure.Framework;
using System;
using Infrastructure.ErrorHandling;

namespace Infrastructure.Framework
{
    public class BaseViewModel : ViewModelBase, IDisposable
    {
        protected ICompositeLogger Logger { get; private set; }
        protected IUIManager UIManager { get; private set; }
        protected ITaskManager TaskManager { get; private set; }
        protected IExceptionHandler ExceptionHandler { get; private set; }

        public BaseViewModel()
        {
            UIManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUIManager>();
            TaskManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>();
            Logger = CommonServiceLocator.ServiceLocator.Current.GetInstance<ICompositeLogger>();
            ExceptionHandler = CommonServiceLocator.ServiceLocator.Current.GetInstance<IExceptionHandler>();
        }


        public virtual void Dispose()
        {
        }
    }
}
