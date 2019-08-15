﻿using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniBar.Common.Workitems.Main.Views
{
    /// <summary>
    /// Interaction logic for MainTab.xaml
    /// </summary>
    public partial class MainTab : DXTabItem
    {
        public MainTab()
        {
            InitializeComponent();
            TabControlStretchView.SetPinMode(this, TabPinMode.Left);
        }

        //private void DXTabItem_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        //{
        //    e.Action = DragAction.Cancel;
        //    e.Handled = true;
        //}
    }
}