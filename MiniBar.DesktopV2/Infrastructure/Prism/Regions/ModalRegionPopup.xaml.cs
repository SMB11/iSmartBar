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
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using Infrastructure.Interface;

namespace Infrastructure.Prism.Regions
{
    /// <summary>
    /// Interaction logic for ModalRegionPopup.xaml
    /// </summary>
    public partial class ModalRegionPopup : ThemedWindow, IPopup
    {
        public ModalRegionPopup()
        {
            InitializeComponent();
        }

        public void SetContent(object content)
        {
            ContentControl.Content = content;
        }
    }
}
