using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace Infrastructure.Utility
{
    public static class UIHelper
    {

        //public static void ExecuteUIThread(Action action)
        //{
        //    ExecuteUIThread(action, System.Windows.Threading.DispatcherPriority.Normal);
        //}



        //public static void ExecuteNonUIThread(Action action)
        //{
        //    if (Application.Current.Dispatcher.CheckAccess())
        //        Task.Run(action);
        //    else
        //        action?.Invoke();
        //}

        //public async static Task<T> ExecuteNonUIThread<T>(Func<T> func)
        //{
        //    if (Application.Current.Dispatcher.CheckAccess())
        //        return await Task.Run<T>(func);
        //    else
        //        return func.Invoke();
        //}

        //public static void ExecuteUIThread(Action action, System.Windows.Threading.DispatcherPriority priority)
        //{
        //    if (!Application.Current.Dispatcher.CheckAccess())
        //    {
        //        Application.Current.Dispatcher.BeginInvoke(priority, new Action(() =>
        //        {
        //            action();
        //        }));

        //    }
        //    else action();
        //}



        //public static DXDialogWindow ShowModal(object content, string title, ResizeMode resizeMode)
        //{

        //    return ShowModal(content, title, resizeMode, new Size(300, 300));
        //}

        //public static DXDialogWindow ShowModal(object content, string title, ResizeMode resizeMode, Size size)
        //{
        //    return ShowModal(content, title, resizeMode, size, new List<UICommand>(), true);
        //}

        //public static DXDialogWindow ShowModal(object content, string title, ResizeMode resizeMode, Size size, List<UICommand> uiCommands, bool returnImmidately)
        //{
        //    DXDialogWindow editWindow = null;
        //    if (uiCommands != null && uiCommands.Count != 0)
        //        editWindow = new DXDialogWindow(title, uiCommands);
        //    else
        //        editWindow = new DXDialogWindow(title);

        //    editWindow.Width = size.Width;
        //    editWindow.Height = size.Height;
        //    editWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //    editWindow.ResizeMode = resizeMode;
        //    editWindow.Content = content;
        //    editWindow.ShowIcon = false;
        //    editWindow.SetParent(Application.Current.MainWindow);

        //    if (returnImmidately)
        //        editWindow.Show();
        //    else
        //        editWindow.ShowDialogWindow();
        //    return editWindow;
        //}


        public static bool TryFocusWindow(Window window, bool activate = true)
        {
            try
            {
                window.Activate();
                window.Focus();
                window.Topmost = true;
                window.Topmost = false;
                IntPtr handle = new WindowInteropHelper(window).Handle;
                if (activate)
                    ActivateMainWindow(handle);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        #region Window Focus Logic


        const int SW_RESTORE = 9;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static void ActivateMainWindow(IntPtr hWnd)
        {

            SetForegroundWindow(hWnd);

            // If program is minimized, restore it.
            if (IsIconic(hWnd))
            {
                ShowWindow(hWnd, SW_RESTORE);
            }
        }

        #endregion
    }
}
