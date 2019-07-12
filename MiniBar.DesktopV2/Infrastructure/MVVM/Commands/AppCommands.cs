using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.MVVM
{
    public static class AppCommands
    {
        private static readonly RoutedUICommand newTabCommand = new RoutedUICommand("Add New Tab", "NewTab", typeof(AppCommands));

        public static ICommand NewTabCommand
        {
            get
            {
                return CommandManager.GetApplicationCommand(Constants.CommandNames.NewTab);
            }
        }
    }
}
