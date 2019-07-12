using DevExpress.Mvvm;
using Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MVVM
{
    public class SecureCommand : DelegateCommand
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
    }
}
