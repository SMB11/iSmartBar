using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure
{
    public class WorkitemManager : DependencyObject
    {

        public static IWorkItem GetOwner(DependencyObject obj)
        {
            return (IWorkItem)obj.GetValue(OwnerProperty);
        }

        public static void SetOwner(DependencyObject obj, IWorkItem value)
        {
            obj.SetValue(OwnerProperty, value);
        }

        private static void TrySetForChildren(DependencyObject obj, IWorkItem value)
        {
            foreach(var child in LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>())
            {
                IWorkItem owner = GetOwner(child);
                if (owner == null)
                    SetOwner(child, value);

            }
        }

        // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.RegisterAttached("Owner", typeof(IWorkItem), typeof(WorkitemManager), new PropertyMetadata(null, new PropertyChangedCallback(OwnerChanged)));

        private static void OwnerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            TrySetForChildren(obj, (IWorkItem)e.NewValue);

            CommandManager.OnOwnerChanged(obj);
        }
    }
}
