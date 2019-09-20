using DevExpress.Xpf.Grid;
using Infrastructure.Interface;
using System.Windows.Controls;

namespace MiniBar.ProductsModule.Workitems.BrandManager.Views
{
    /// <summary>
    /// Interaction logic for BrandManagerView.xaml
    /// </summary>
    public partial class BrandManagerView : UserControl, IGridView
    {
        public BrandManagerView()
        {
            InitializeComponent();
        }

        public GridControl Grid => grid;
    }
}
