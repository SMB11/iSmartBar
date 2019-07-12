using DevExpress.Mvvm;
using Infrastructure.Api;
using Infrastructure.Connection;
using Infrastructure.Util;
using Infrastructure.MVVM;
using MiniBar.Common.Resources;
using MiniBar.EntityViewModels.Products;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MiniBar.EntityViewModels.Global;

namespace MiniBar.ProductsModule.Workitems.CategoryManager.Views
{
    partial class CategoryManagerViewModel
    {
        private SecureCommand nameEditCommand;
        public SecureCommand NameEditCommand
        {
            get
            {
                if (nameEditCommand == null)
                    nameEditCommand = new SecureCommand(StartEditName, CanEditObject);
                return nameEditCommand;
            }
        }

        protected override void OnReadOnlyChanged()
        {
            base.OnReadOnlyChanged();

            NameEditCommand.RaiseCanExecuteChanged();
        }

        private void StartEditName()
        {
            LanguageEdit languageEdit = new LanguageEdit();
            LanguageEditViewModel vm = (LanguageEditViewModel)languageEdit.DataContext;
            vm.SetData(CurrentItemDetails.Names ?? new BindableDictionary<string>());
            UICommand saveCommand = new UICommand
            {
                Caption = "Save",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>((c) => SaveName(c, languageEdit.DataContext as LanguageEditViewModel))
            };
            UICommand cancleCommand = new UICommand
            {
                Caption = "Cancel",
                IsCancel = true
            };
            UIHelper.ShowModal(languageEdit, "Edit Name", ResizeMode.CanResizeWithGrip, new Size(300, 300), new List<UICommand> { saveCommand, cancleCommand }, false);
        }


        private bool CanEditObject()
        {
            return !IsReadOnly;
        }

        private void SaveName(CancelEventArgs arg, LanguageEditViewModel vm)
        {
            if (!vm.Validate())
            {
                arg.Cancel = true;
            }
            else
            {
                CurrentItemDetails.Names = new EntityViewModels.Global.BindableDictionary<string>(vm.GetData());
            }
        }

        
        
    }
}
