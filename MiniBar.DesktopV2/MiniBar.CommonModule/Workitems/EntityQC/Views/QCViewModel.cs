using Infrastructure.Framework;
using Infrastructure.Interface;
using System.Collections.ObjectModel;

namespace MiniBar.Common.Workitems.EntityQC.Views
{
    public class QCViewModel : WorkitemGridViewModel, IGridViewModel
    {

        private bool isListEnabled = true;

        public bool IsListEnabled
        {
            get { return isListEnabled && !IsListLoading; }
            set { SetProperty(ref isListEnabled, value, nameof(IsListEnabled)); }
        }


        private bool isListLoading;

        public bool IsListLoading
        {
            get { return isListLoading; }
            set
            {
                SetProperty(ref isListLoading, value, nameof(IsListLoading));
                RaisePropertyChanged(nameof(IsListEnabled));
            }
        }

        private object currentItem;
        public object CurrentItem
        {
            get { return currentItem; }
            set
            {
                SetProperty(ref currentItem, value, nameof(CurrentItem));
            }
        }


        private ObservableCollection<object> list;
        public ObservableCollection<object> List
        {
            get { return list; }
            set
            {
                SetProperty(ref list, value, nameof(List));
            }
        }

        private SecureCommand removeCommand;
        public SecureCommand RemoveCommand =>
            removeCommand ?? (removeCommand = Disposable(new SecureCommand(ExecuteRemoveCommand, CanExecuteRemoveCommand)));

        void ExecuteRemoveCommand()
        {
            if (List.Contains(CurrentItem))
                List.Remove(CurrentItem);
        }

        bool CanExecuteRemoveCommand()
        {
            return CurrentItem != null;
        }


    }
}