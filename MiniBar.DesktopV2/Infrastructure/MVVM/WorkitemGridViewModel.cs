using DevExpress.Xpf.Grid;
using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MVVM
{
    public class WorkitemGridViewModel : WorkitemViewModel, IGridViewModel
    {

        public GridControl Grid { get; set; }


        private SecureCommand searchCommand;
        public SecureCommand SearchCommand =>
            searchCommand ?? (searchCommand = new SecureCommand(Search, CanExecuteSearchCommand));

        private SecureCommand collapseAllCommand;
        public SecureCommand CollapseAllCommand =>
            collapseAllCommand ?? (collapseAllCommand = new SecureCommand(ExecuteCollapseAllCommand, CanExecuteCollapseAllCommand));


        private SecureCommand expandAllCommand;
        public SecureCommand ExpandAllCommand =>
            expandAllCommand ?? (expandAllCommand = new SecureCommand(ExecuteExpandAllCommand, CanExecuteExpandAllCommand));

        protected virtual bool CanExecuteCollapseAllCommand()
        {
            return true;
        }

        protected virtual bool CanExecuteExpandAllCommand()
        {
            return true;
        }

        protected virtual bool CanExecuteSearchCommand()
        {
            return true;
        }


        protected virtual void Search()
        {
            Grid.View.ShowSearchPanel(true);
        }


        protected virtual void ExecuteCollapseAllCommand()
        {
            for (int i = 0; i < Grid.VisibleRowCount; i++)
            {
                var handle = Grid.GetRowHandleByVisibleIndex(i);
                Grid.CollapseMasterRow(handle);
            }
            Grid.CollapseAllGroups();
        }

        protected virtual void ExecuteExpandAllCommand()
        {
            for (int i = 0; i < Grid.VisibleRowCount; i++)
            {
                var handle = Grid.GetRowHandleByVisibleIndex(i);
                Grid.ExpandMasterRow(handle);
            }
            Grid.ExpandAllGroups();
        }
    }
}
