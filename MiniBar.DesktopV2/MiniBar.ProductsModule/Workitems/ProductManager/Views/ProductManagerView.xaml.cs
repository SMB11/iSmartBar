using DevExpress.Xpf.Grid;
using Infrastructure.Interface;
using System.Windows.Controls;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Views
{
    /// <summary>
    /// Interaction logic for ProductManagerView.xaml
    /// </summary>
    public partial class ProductManagerView : UserControl, IGridView
    {

        public ProductManagerView()
        {
            InitializeComponent();

        }

        public GridControl Grid => grid;

        private void CategoryEdit_EditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e)
        {
            ProductManagerViewModel dc = DataContext as ProductManagerViewModel;

            e.Handled = true;
            if (e.NewValue is int)
            {
                int id = (int)e.NewValue;
                e.IsCancel = id != 0 && !dc.IsValidCategoryID(id);
            }
        }
    }
}
