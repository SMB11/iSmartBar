using DevExpress.Xpf.Grid;
using Infrastructure.Interface;
using System.Windows.Controls;

namespace MiniBar.ProductsModule.Workitems.BrandQC.Views
{
    /// <summary>
    /// Interaction logic for BrandQCView.xaml
    /// </summary>
    public partial class BrandQCView : UserControl, IGridView
    {

        public BrandQCView(IWorkItem owner)
        {
            InitializeComponent();
        }

        public GridControl Grid => grid;
    }
}
