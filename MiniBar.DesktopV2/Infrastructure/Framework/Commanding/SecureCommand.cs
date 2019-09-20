using DevExpress.Mvvm;
using Infrastructure.Security;
using System;

namespace Infrastructure.Framework
{
    /// <summary>
    /// Delegate command with secure implementation
    /// </summary>
    public class SecureCommand : DelegateCommand, IDisposable
    {
        public SecureCommand(Action executeMethod, bool? useCommandManager = null) : base(executeMethod, SecureCanExecute, useCommandManager)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureCommand(Action executeMethod, Func<bool> canExecuteMethod, bool? useCommandManager = null) : base(executeMethod, () => canExecuteMethod() && SecureCanExecute(), useCommandManager)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }


        private void HandleAppPrincipalChanged(object sender, EventArgs e)
        {
            this.RaiseCanExecuteChanged();
        }

        private static bool SecureCanExecute()
        {
            return AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated;
        }

        public void Dispose()
        {
            AppSecurityContext.AppPrincipalChanged -= HandleAppPrincipalChanged;
        }
    }
}
