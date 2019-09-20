using DevExpress.Xpf.Grid;
using Infrastructure.Interface;
using System.Windows.Controls;

namespace MiniBar.ProductsModule.Workitems.CategoryQC.Views
{
    /// <summary>
    /// Interaction logic for CategoryQCView.xaml
    /// </summary>
    public partial class CategoryQCView : UserControl, IGridView
    {

        public CategoryQCView(IWorkItem owner)
        {
            InitializeComponent();

        }

        public GridControl Grid => grid;
    }
}
