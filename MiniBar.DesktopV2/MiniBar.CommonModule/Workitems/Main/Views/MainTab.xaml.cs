using DevExpress.Xpf.Core;

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
