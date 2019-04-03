using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Helpers
{
    public static class UIHelper
    {

        public static void ExecuteUIThread(Action action)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                {
                    action();
                }));

            }
            else action();
        }

        public static MessageBoxResult ShowMessageBox(string message, string caption, System.Windows.MessageBoxButton buttons = System.Windows.MessageBoxButton.OK)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                return Application.Current.Dispatcher.Invoke(() => {
                    return DXMessageBox.Show(message, caption, buttons);

                });
            }
            else
            {
                return DXMessageBox.Show(message, caption, buttons);
            }
        }

        public static void ShowErrorMessageBox(string message)
        {
            ShowMessageBox(message, "Error", MessageBoxButton.OK);
        }
    }
}
