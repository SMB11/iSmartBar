﻿using Infrastructure.Interface;
using Infrastructure.Utility;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Workitems.Strategies.Launch
{
    /// <summary>
    /// Launch child workitem strategy
    /// </summary>
    internal class ChildWorkitemLaunchStrategy : WorkitemLaunchStrategy
    {
        public ChildWorkitemLaunchStrategy(ContextService currentContextService, IWorkItem workItem, IWorkItem parent = null, object data = null) : base(currentContextService, workItem, parent, data)
        {
        }

        /// <summary>
        /// Launch the child workitem
        /// </summary>
        protected override async Task Execute()
        {
            // if parent is modal but noraml workitem launch is requested throw ArgumentException
            if (Parent.IsModal && !ShouldOpenModal)
                throw new ArgumentException("Child workitem of modal must be modal");
            // get workitem type
            Type type = Workitem.GetType();
            // Set parent
            Workitem.Parent = Parent;
            // if should open modal set window owner to pernt.window or mainwindow
            if (ShouldOpenModal)
                Application.Current.Dispatcher.InvokeIfNeeded(() => ((Window)Workitem.Window).Owner = ((Window)Parent.Window) ?? Application.Current.MainWindow);

            await RunWorkitem().ConfigureAwait(false);

        }
    }
}
