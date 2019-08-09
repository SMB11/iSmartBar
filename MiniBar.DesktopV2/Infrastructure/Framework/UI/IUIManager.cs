using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Framework
{
    public interface IUIManager
    {
        MessageBoxResult ShowMessageBox(string message, string caption, System.Windows.MessageBoxButton buttons = System.Windows.MessageBoxButton.OK);
        void Error(string message);
        bool AskForConfirmation(string message);
    }
}
