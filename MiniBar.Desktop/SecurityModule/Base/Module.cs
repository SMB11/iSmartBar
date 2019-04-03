using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Ioc;
using System.Windows.Input;
using Prism.Events;
using System.Reflection;
using DevExpress.Mvvm;
using Infrastructure.Interface;
using Infrastructure.Prism.Events;

namespace Security
{
    public abstract class Module : IModule
    {
        #region Private Fields

        private ICurrentContextService _currentContextService;

        private List<DelegateCommand> secureCommands = new List<DelegateCommand>();
        private IEventAggregator _eventAggregator;

        #endregion

        #region Private Methods

        private bool SecureCanExecute()
        {
            SecureModuleAttribute secureAttribute = this.GetType().GetCustomAttributes<SecureModuleAttribute>().FirstOrDefault();
            if (secureAttribute != null)
            {
                return AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated;
            }
            return true;
        }

        private void HandleUserAuthentication()
        {
            this.secureCommands.ForEach(c => c.RaiseCanExecuteChanged());
        }

        #endregion

        #region Public and Protected Methods
        public virtual void OnInitialized(IContainerProvider containerProvider)
        {

            this._currentContextService = containerProvider.Resolve< ICurrentContextService>();
            this._eventAggregator = containerProvider.Resolve<IEventAggregator>();
            this._eventAggregator.GetEvent<AuthenticationStateChanged>().Subscribe(HandleUserAuthentication);
        }

        public virtual void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
        
        protected ICommand ToCommand(Action method)
        {
            return new DelegateCommand(method);
        }


        protected ICommand ToSecureCommand(Action method)
        {
            DelegateCommand command = new DelegateCommand(method, SecureCanExecute);
            secureCommands.Add(command);
            return command;
        }


        #endregion

        #region Properties
        public abstract string Name { get; }
        
        protected ICurrentContextService CurrentContextService { get { return _currentContextService; } }
        protected IEventAggregator EventAggregator { get { return _eventAggregator; } }

        #endregion
        
    }
}
