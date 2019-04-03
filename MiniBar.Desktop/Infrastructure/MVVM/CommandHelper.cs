using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.MVVM
{
    public static class CommandHelper
    {
        public static string Format(params string[] names)
        {
            string res = "";
            foreach(string name in names)
            {
                res += name + ".";
            }
            return res.Substring(0, res.Length - 1);
        }


        public static ICommand CombineCommands(ICommand command1, ICommand command2)
        {
            CompositeCommand command = new CompositeCommand();
            command.RegisterCommand(command1);
            command.RegisterCommand(command2);
            return command;
        }
    }
}
