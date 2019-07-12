using DevExpress.Mvvm;
using Infrastructure.Api;
using Infrastructure.Connection;
using Infrastructure.Util;
using Infrastructure.MVVM;
using MiniBar.Common.Resources;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MiniBar.Common.MVVM;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Views
{
    /// <summary>
    /// Details Part
    /// </summary>
    partial class ProductManagerViewModel : ObjectManagerViewModel<ProductViewModel, ProductUploadViewModel>
    {
        private SecureCommand nameEditCommand;
        public SecureCommand NameEditCommand
        {
            get
            {
                if (nameEditCommand == null)
                    nameEditCommand = new SecureCommand(StartEditName, CanEditObject,false);
                return nameEditCommand;
            }
        }
        
        private SecureCommand descEditCommand;
        public SecureCommand DescriptionEditCommand
        {
            get
            {
                if (descEditCommand == null)
                    descEditCommand = new SecureCommand(StartEditDescription, CanEditObject, false);
                return descEditCommand;
            }
        }
        
        protected override void OnReadOnlyChanged()
        {
            base.OnReadOnlyChanged();

            DescriptionEditCommand.RaiseCanExecuteChanged();
            NameEditCommand.RaiseCanExecuteChanged();
        }

        private void StartEditName()
        {
            LanguageEdit languageEdit = new LanguageEdit();
            LanguageEditViewModel vm = (LanguageEditViewModel)languageEdit.DataContext;
            vm.SetData(CurrentItemDetails.Names ?? new Dictionary<string, string>());
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

        private void StartEditDescription()
        {
            LanguageEdit languageEdit = new LanguageEdit();
            LanguageEditViewModel vm = languageEdit.DataContext as LanguageEditViewModel;
            vm.SetData(CurrentItemDetails.Description ?? new Dictionary<string, string>());
            UICommand saveCommand = new UICommand
            {
                Caption = "Save",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>((c) => SaveDescription(c, languageEdit.DataContext as LanguageEditViewModel))
            };
            UICommand cancleCommand = new UICommand
            {
                Caption = "Cancel",
                IsCancel = true
            };
            UIHelper.ShowModal(languageEdit, "Edit Description", ResizeMode.CanResizeWithGrip, new Size(300, 300), new List<UICommand> { saveCommand, cancleCommand }, false);

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
                CurrentItemDetails.Names = vm.GetData();
            }
        }

        private void SaveDescription(CancelEventArgs arg, LanguageEditViewModel vm)
        {
            if (!vm.Validate())
            {
                arg.Cancel = true;
            }
            else
            {
                CurrentItemDetails.Description = vm.GetData();
            }
        }

        protected override ProductUploadViewModel CreateEmptyDetails()
        {
            return new ProductUploadViewModel();
        }
        
    }
}
