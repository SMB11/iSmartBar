using DevExpress.Mvvm;
using Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MVVM
{
    public class SecureAsyncCommand : AsyncCommand
    {
        public SecureAsyncCommand(Func<Task> executeMethod) : base(executeMethod, SecureCanExecute)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureAsyncCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, () => canExecuteMethod() && SecureCanExecute())
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
    }
}
