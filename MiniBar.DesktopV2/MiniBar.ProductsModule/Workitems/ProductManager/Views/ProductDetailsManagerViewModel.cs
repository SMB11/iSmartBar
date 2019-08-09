
using MiniBar.EntityViewModels.Products;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using MiniBar.Common.Workitems.LanguageEdit;
using MiniBar.Common.Workitems.ObjectManager;
using Infrastructure.Interface;
using Infrastructure.Framework;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Views
{
    /// <summary>
    /// Details Part
    /// </summary>
    partial class ProductManagerViewModel : ObjectManagerViewModel<ProductViewModel, ProductUploadViewModel>
    {
        private SecureAsyncCommand nameEditCommand;
        public SecureAsyncCommand NameEditCommand
        {
            get
            {
                if (nameEditCommand == null)
                    nameEditCommand = Disposable(new SecureAsyncCommand(StartEditName, CanEditObject));
                return nameEditCommand;
            }
        }
        
        private SecureAsyncCommand descEditCommand;
        public SecureAsyncCommand DescriptionEditCommand
        {
            get
            {
                if (descEditCommand == null)
                    descEditCommand = Disposable(new SecureAsyncCommand(StartEditDescription, CanEditObject));
                return descEditCommand;
            }
        }
        
        protected override void OnReadOnlyChanged()
        {
            base.OnReadOnlyChanged();

            DescriptionEditCommand.RaiseCanExecuteChanged();
            NameEditCommand.RaiseCanExecuteChanged();
        }

        private async Task StartEditName()
        {
            IObservable<WorkitemEventArgs> channel =  await CurrentContextService.LaunchModalWorkItem<LanguageEditWorkitem>(CurrentItemDetails.Names ?? new Dictionary<string, string>(), WorkItem);
            channel.Subscribe(data => CurrentItemDetails.Names = (IDictionary<string, string>)data.Data);
            
        }

        private async Task StartEditDescription()
        {
            IObservable<WorkitemEventArgs> channel = await CurrentContextService.LaunchModalWorkItem<LanguageEditWorkitem>(CurrentItemDetails.Description ?? new Dictionary<string, string>(), WorkItem);
            channel.Subscribe(data => CurrentItemDetails.Description = (IDictionary<string, string>)data.Data);
        }

        private bool CanEditObject()
        {
            return !IsReadOnly;
        }

        protected override ProductUploadViewModel CreateEmptyDetails()
        {
            return new ProductUploadViewModel();
        }
        
    }
}
