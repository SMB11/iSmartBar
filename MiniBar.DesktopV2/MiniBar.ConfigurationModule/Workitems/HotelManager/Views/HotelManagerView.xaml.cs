using DevExpress.Xpf.Grid;
using Infrastructure.Interface;
using System.Windows.Controls;

namespace MiniBar.ConfigurationModule.Workitems.HotelManager.Views
{
    /// <summary>
    /// Interaction logic for HotelManagerView.xaml
    /// </summary>
    public partial class HotelManagerView : UserControl, IGridView
    {
        public HotelManagerView()
        {
            InitializeComponent();
        }

        public GridControl Grid => grid;
    }
}
