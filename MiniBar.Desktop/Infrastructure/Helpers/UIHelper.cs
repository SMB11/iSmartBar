using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Infrastructure.Helpers
{
    public static class UIHelper
    {

        private static DXDialogWindow currentDialog;

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


        public static void ShowModal(object content, string title, ResizeMode resizeMode)
        {

            ShowModal(content, title, resizeMode, new Size(300, 300));
        }

        public static void ShowModal(object content, string title, ResizeMode resizeMode, Size size)
        {
            ShowModal(content, title, resizeMode, size);
        }

        public static void ShowModal(object content, string title, ResizeMode resizeMode, Size size, List<UICommand> uiCommands)
        {
            DXDialogWindow editWindow = null;
            if(uiCommands != null && uiCommands.Count != 0)
                editWindow = new DXDialogWindow(title, uiCommands);
            else
                editWindow = new DXDialogWindow(title);

            editWindow.Width = size.Width;
            editWindow.Height = size.Height;
            editWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            editWindow.ResizeMode = resizeMode;
            editWindow.Content = content;
            editWindow.ShowIcon = false;
            editWindow.SetParent(Application.Current.MainWindow);
            currentDialog = editWindow;

            editWindow.ShowDialogWindow();
        }

        public static void CloseCurrentDialog()
        {
            currentDialog?.Close();
        }

        public static void ShowErrorMessageBox(string message)
        {
            ShowMessageBox(message, "Error", MessageBoxButton.OK);
        }
    }
}
