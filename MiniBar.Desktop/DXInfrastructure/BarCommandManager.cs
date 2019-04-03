using DevExpress.Xpf.Bars;
using DevExpress.Xpf.NavBar;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DXInfrastructure
{
    public class BarCommandManager
    {

        public static string GetCommandName(DependencyObject obj)
        {
            return (string)obj.GetValue(CommandNameProperty);
        }
        public static void SetCommandName(DependencyObject obj, string value)
        {
            obj.SetValue(CommandNameProperty, value);
        }
        public static readonly DependencyProperty CommandNameProperty =
            DependencyProperty.RegisterAttached("CommandName", typeof(string), typeof(BarCommandManager), new PropertyMetadata(null, OnCommandNameChanged));
        private static void OnCommandNameChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            if (!DesignerProperties.GetIsInDesignMode(element))
            {
                CreateCommand(element, args.NewValue as string);
            }
        }

        private static void CreateCommand(DependencyObject element, string command)
        {
            if(element is NavBarItem)
            {
                (element as NavBarItem).Command = GetCompositeCommandByName(command);
            }
            else if(element is BarCheckItem)
            {
                BarCheckItem item = (element as BarCheckItem);
                CompositeCommand composite = GetCompositeCommandByName(command);
                item.Command = WrapCommandAddParameter(composite, () => item.IsChecked);
            }
            else if (element is BarItem)
                (element as BarItem).Command = GetCompositeCommandByName(command);
            Debug.WriteLine("Command " + command + " has been created.");

        }

        private static ICommand WrapCommandAddParameter(ICommand command, Func<object> getParam)
        {
            DelegateCommand wrapped = new DelegateCommand(() => {
                command.Execute(getParam());
            },
            () => command.CanExecute(getParam()));
            command.CanExecuteChanged += (o, e) => wrapped.RaiseCanExecuteChanged();
            return wrapped;
        }

        private static Dictionary<string, CompositeCommand> commandMap = new Dictionary<string, CompositeCommand>();

        public static void RegisterCommand(string commandName, ICommand command)
        {
            GetCompositeCommandByName(commandName).RegisterCommand(command);
        }

        public static void UnregisterCommand(string commandName, ICommand command)
        {
            GetCompositeCommandByName(commandName).UnregisterCommand(command);
        }

        public static void RegisterCommand(string commandName, Action action)
        {
            GetCompositeCommandByName(commandName).RegisterCommand(new DelegateCommand(action));
        }

        private static CompositeCommand GetCompositeCommandByName(string name)
        {
            if (!commandMap.ContainsKey(name))
                commandMap.Add(name, new CompositeCommand());
            return commandMap[name];
        }
    }
}
