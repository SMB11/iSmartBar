using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Infrastructure.Extensions
{
    public static class UIExtensions
    {
        public static void InvokeIfNeeded(this Dispatcher dispatcher, Action action)
        {
            if (dispatcher.CheckAccess()) {
                action?.Invoke();
            }
            else
            {
                dispatcher.Invoke(action);
            }

        }

        public static void InvokeIfNeeded<T>(this Dispatcher dispatcher, Action<T> action, T param)
        {
            if (dispatcher.CheckAccess())
            {
                action?.Invoke(param);
            }
            else
            {
                dispatcher.Invoke(action, param);
            }

        }

        public static void TryClose(this Window window)
        {

            try
            {
                window.Close();
            }
            catch (Exception e)
            {

            }
        }
    }
}
