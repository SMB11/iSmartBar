using DevExpress.Xpf.Grid;
using Infrastructure.Interface;
using System.Windows.Controls;

namespace MiniBar.ConfigurationModule.Workitems.HotelQC.Views
{
    /// <summary>
    /// Interaction logic for HotelQCView.xaml
    /// </summary>
    public partial class HotelQCView : UserControl, IGridView
    {

        public HotelQCView(IWorkItem owner)
        {
            InitializeComponent();
        }

        public GridControl Grid => grid;
    }
}
