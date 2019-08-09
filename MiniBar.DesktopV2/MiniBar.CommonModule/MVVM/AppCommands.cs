using MiniBar.Common.Constants;
using System.Windows.Input;

namespace MiniBar.Common
{
    public static class AppCommands
    {

        public static ICommand NewTabCommand
        {
            get
            {
                return Infrastructure.CommandManager.GetApplicationCommand(CommandNames.NewTab);
            }
        }
    }
}
