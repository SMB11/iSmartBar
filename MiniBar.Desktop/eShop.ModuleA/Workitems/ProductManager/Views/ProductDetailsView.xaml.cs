using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using Infrastructure.Resources;
using MiniBar.EntityViewModels.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Views
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class ProductDetailsView : UserControl
    {
        public ProductDetailsView()
        {
            InitializeComponent();
            
            categoryEdit.EditValueChanging += CategoryEdit_EditValueChanging;
        }

        private void CategoryEdit_EditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e)
        {
            ProductDetailsViewModel dc = DataContext as ProductDetailsViewModel;
            
            e.Handled = true;
            if (e.NewValue is int)
            {
                int id = (int)e.NewValue;
                e.IsCancel = id != 0 && !dc.IsValidCategoryID(id);
            }
        }

        private void ButtonEdit_EditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e)
        {
            e.IsCancel = true;
            e.Handled = true;
        }
    }
}
