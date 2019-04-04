using DevExpress.Mvvm;
using Infrastructure.Helpers;
using Infrastructure.MVVM.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Workitems
{
    public abstract class CrudManagerWorkitem: Workitem
    {

        #region Commands
        private SecureCommand _deleteCommand;

        public SecureCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new SecureCommand(Delete, CanDelete);
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

        private SecureCommand _saveCommand;

        public SecureCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new SecureCommand(Save, CanSave);
                return _saveCommand;
            }
        }
        #endregion

        public CrudManagerWorkitem(IContainerExtension container) : base(container)
        {
        }

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

        protected virtual void Delete()
        {
        }

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

        protected virtual void Save()
        {
        }


        protected virtual bool CanAdd()
        {
            return true;
        }

        protected virtual void Add()
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
