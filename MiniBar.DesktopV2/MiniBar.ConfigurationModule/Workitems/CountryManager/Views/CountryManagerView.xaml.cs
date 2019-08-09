using DevExpress.Xpf.Grid;
using Infrastructure.Interface;
using System.Windows.Controls;

namespace MiniBar.ConfigurationModule.Workitems.CountryManager.Views
{
    /// <summary>
    /// Interaction logic for BrandManagerView.xaml
    /// </summary>
    public partial class CountryManagerView : UserControl, IGridView
    {
        public CountryManagerView()
        {
            InitializeComponent();
        }

        public GridControl Grid => grid;
    }
}
