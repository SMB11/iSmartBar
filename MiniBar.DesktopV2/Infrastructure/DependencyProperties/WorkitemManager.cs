using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Bars;
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
            if (obj is RibbonControl)
            {
                RibbonControl ribbon = obj as RibbonControl;
                
                foreach (DependencyObject child in ribbon.ActualCategories.OfType<DependencyObject>())
                    SetOwner(child, value);
            }
            else if (obj is RibbonPageGroup)
            {
                RibbonPageGroup group = obj as RibbonPageGroup;
                foreach(DependencyObject child in group.Items.OfType<DependencyObject>())
                    SetOwner(child, value);
            }
            else if (obj is RibbonDefaultPageCategory)
            {
                RibbonDefaultPageCategory category = obj as RibbonDefaultPageCategory;
                foreach (DependencyObject child in category.Pages.OfType<DependencyObject>())
                    SetOwner(child, value);
            }
            else if(obj is RibbonPageCategory)
            {
                RibbonPageCategory category = obj as RibbonPageCategory;
                foreach (DependencyObject child in category.Pages.OfType<DependencyObject>())
                    SetOwner(child, value);
            }
            else if (obj is RibbonPage)
            {
                RibbonPage page = obj as RibbonPage;
                foreach (DependencyObject child in page.Groups.OfType<DependencyObject>())
                    SetOwner(child, value);
            }
            else if (obj is BarItemLinkHolderBase)
            {
                BarItemLinkHolderBase items = obj as BarItemLinkHolderBase;
                foreach (DependencyObject child in items.Items.OfType<DependencyObject>())
                    SetOwner(child, value);
            }
        }

        // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.RegisterAttached("Owner", typeof(IWorkItem), typeof(WorkitemManager), new PropertyMetadata(null, new PropertyChangedCallback(OwnerChanged)));

        private static void OwnerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            TrySetForChildren(obj, (IWorkItem)e.NewValue);
            if (obj is KeyToCommand)
            {

            }

            CommandManager.OnOwnerChanged(obj);
        }
    }
}
