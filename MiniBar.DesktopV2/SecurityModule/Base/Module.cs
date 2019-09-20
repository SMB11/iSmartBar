using Infrastructure.Interface;
using Infrastructure.Security;
using Prism.Ioc;
using Prism.Modularity;
using System.Linq;
using System.Reflection;

namespace Security
{
    public abstract class Module : IModule
    {
        #region Private Fields

        private IContextService _currentContextService;

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

            this._currentContextService = containerProvider.Resolve<IContextService>();
        }

        public virtual void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        #endregion

        #region Properties
        public abstract string Name { get; }

        protected IContextService CurrentContextService { get { return _currentContextService; } }

        #endregion

    }
}
