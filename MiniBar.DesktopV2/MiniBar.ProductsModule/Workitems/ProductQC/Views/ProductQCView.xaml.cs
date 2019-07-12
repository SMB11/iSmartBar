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
            
            WorkitemManager.SetOwner(ribbon, owner);
        }

        public GridControl Grid => grid;
    }
}
