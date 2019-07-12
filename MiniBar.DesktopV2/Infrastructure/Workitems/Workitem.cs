using DevExpress.Xpf.Ribbon;
using Infrastructure.Constants;
using Infrastructure.Interface;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Windows;
using Unity.Attributes;

namespace Infrastructure.Workitems
{
    public abstract class Workitem : WorkitemBase
    {
        public Workitem(IContainerExtension container) : base(container)
        {
            Window = Application.Current.MainWindow;
        }

        public override void Run()
        {
            base.Run();
        }
        
    }
}
