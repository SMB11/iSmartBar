using DevExpress.Xpf.Grid;
using Infrastructure;
using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
