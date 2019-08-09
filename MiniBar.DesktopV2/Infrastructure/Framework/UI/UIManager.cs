using DevExpress.Xpf.Core;
using System.Windows;

namespace Infrastructure.Framework
{
    public class UIManager : IUIManager
    {
        public MessageBoxResult ShowMessageBox(string message, string caption, System.Windows.MessageBoxButton buttons = System.Windows.MessageBoxButton.OK)
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

        public void Error(string message)
        {
            ShowMessageBox(message, "Error", MessageBoxButton.OK);
        }

        public bool AskForConfirmation(string message)
        {
            return ShowMessageBox(message, "Confirmation Needed", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes;
        }
    }
}
