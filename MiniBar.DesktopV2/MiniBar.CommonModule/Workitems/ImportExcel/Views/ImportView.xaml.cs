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

namespace MiniBar.Common.Workitems.ImportExcel.Views
{
    /// <summary>
    /// Interaction logic for ImportView.xaml
    /// </summary>
    public partial class ImportView : UserControl
    {
        public ImportView()
        {
            InitializeComponent();
            //WorkitemManager.SetOwner(this, owner);
            //WorkitemManager.SetOwner(ribbon, owner);
        }
    }
}
