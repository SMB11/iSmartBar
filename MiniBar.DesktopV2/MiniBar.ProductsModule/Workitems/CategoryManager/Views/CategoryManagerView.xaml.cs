using DevExpress.Xpf.Grid;
using Infrastructure.Interface;
using System.Windows.Controls;

namespace MiniBar.ProductsModule.Workitems.CategoryManager.Views
{
    /// <summary>
    /// Interaction logic for CategoryManagerView.xaml
    /// </summary>
    public partial class CategoryManagerView : UserControl, IGridView
    {
        public CategoryManagerView()
        {
            InitializeComponent();
        }

        public GridControl Grid => grid;
    }
}
