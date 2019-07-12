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
using Infrastructure.Security;

namespace Security
{
    public abstract class Module : IModule
    {
        #region Private Fields

        private ICurrentContextService _currentContextService;

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

        #endregion

        #region Public and Protected Methods
        public virtual void OnInitialized(IContainerProvider containerProvider)
        {

            this._currentContextService = containerProvider.Resolve< ICurrentContextService>();
        }

        public virtual void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
        
        #endregion

        #region Properties
        public abstract string Name { get; }
        
        protected ICurrentContextService CurrentContextService { get { return _currentContextService; } }

        #endregion
        
    }
}
