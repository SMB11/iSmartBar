using Infrastructure.Util;
using System.Threading.Tasks;

namespace Infrastructure.MVVM
{
    public abstract class CrudManagerViewModel: BaseViewModel
    {

        #region Commands
        private SecureAsyncCommand _deleteCommand;

        public SecureAsyncCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new SecureAsyncCommand(Delete, CanDelete);
                return _deleteCommand;
            }
        }

        private SecureCommand _editCommand;

        public SecureCommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new SecureCommand(Edit, CanEdit);
                return _editCommand;
            }
        }

        private SecureCommand _cancelCommand;

        public SecureCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new SecureCommand(CancelEditing, CanCancelEditing);
                return _cancelCommand;
            }
        }

        private SecureCommand _addCommand;

        public SecureCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new SecureCommand(Add, CanAdd);
                return _addCommand;
            }
        }


        private SecureCommand _addCopyCommand;

        public SecureCommand AddCopyCommand
        {
            get
            {
                if (_addCopyCommand == null)
                    _addCopyCommand = new SecureCommand(AddCopy, CanAddCopy);
                return _addCopyCommand;
            }
        }

        private SecureAsyncCommand _saveCommand;

        public SecureAsyncCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new SecureAsyncCommand(Save, CanSave);
                return _saveCommand;
            }
        }
        #endregion

        #region Private/Protected

        protected void UpdateCrudCommands()
        {
            UIHelper.ExecuteUIThread(() =>
            {
                EditCommand.RaiseCanExecuteChanged();
                SaveCommand.RaiseCanExecuteChanged();
                CancelCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            });
        }

        protected virtual bool CanDelete()
        {
            return true;
        }

        protected abstract Task Delete();

        protected virtual bool CanEdit()
        {
            return true;
        }

        protected virtual void Edit()
        {
        }


        protected virtual bool CanSave()
        {
            return true;
        }

        protected abstract Task Save();


        protected virtual bool CanAdd()
        {
            return true;
        }

        protected virtual void Add()
        {
        }


        protected virtual bool CanAddCopy()
        {
            return true;
        }

        protected virtual void AddCopy()
        {
        }

        protected virtual bool CanCancelEditing()
        {
            return true;
        }

        protected virtual void CancelEditing()
        {
        }

        #endregion
    }
}
