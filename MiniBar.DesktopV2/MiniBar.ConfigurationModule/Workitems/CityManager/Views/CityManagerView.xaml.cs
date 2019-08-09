using DevExpress.Xpf.Grid;
using Infrastructure.Interface;
using System.Windows.Controls;

namespace MiniBar.ConfigurationModule.Workitems.CityManager.Views
{
    /// <summary>
    /// Interaction logic for BrandManagerView.xaml
    /// </summary>
    public partial class CityManagerView : UserControl, IGridView
    {
        public CityManagerView()
        {
            InitializeComponent();
        }

        public GridControl Grid => grid;
    }
}
