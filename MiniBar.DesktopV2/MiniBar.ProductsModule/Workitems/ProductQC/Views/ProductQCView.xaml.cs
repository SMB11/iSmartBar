using DevExpress.Xpf.Grid;
using Infrastructure.Interface;
using System.Windows.Controls;

namespace MiniBar.ProductsModule.Workitems.ProductQC.Views
{
    /// <summary>
    /// Interaction logic for ProductQCView.xaml
    /// </summary>
    public partial class ProductQCView : UserControl, IGridView
    {

        public ProductQCView(IWorkItem owner)
        {
            InitializeComponent();
        }

        public GridControl Grid => grid;
    }
}
