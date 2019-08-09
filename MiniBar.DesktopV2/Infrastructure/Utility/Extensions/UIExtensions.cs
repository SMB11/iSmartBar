using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Infrastructure.Utility
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


        public static Task InvokeAsyncIfNeeded(this Dispatcher dispatcher, Func<Task> action)
        {
            if (dispatcher.CheckAccess())
            {
                return action?.Invoke();
            }
            else
            {
                return  dispatcher.Invoke(action);
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

        public static bool TryClose(this IWindow window)
        {

            try
            {
                window.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
