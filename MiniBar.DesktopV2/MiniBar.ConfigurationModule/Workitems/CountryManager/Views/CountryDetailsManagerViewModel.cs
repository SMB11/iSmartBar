using Infrastructure.Framework;
using Infrastructure.Interface;

using MiniBar.Common.Workitems.LanguageEdit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBar.ConfigurationModule.Workitems.CountryManager.Views
{
    partial class CountryManagerViewModel
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

        protected override void OnReadOnlyChanged()
        {
            base.OnReadOnlyChanged();

            NameEditCommand.RaiseCanExecuteChanged();
        }

        private async Task StartEditName()
        {
            IObservable<WorkitemEventArgs> channel = await CurrentContextService.LaunchModalWorkItem<LanguageEditWorkitem>(CurrentItemDetails.Names ?? new Dictionary<string, string>(), WorkItem);
            channel.Subscribe(data =>
                CurrentItemDetails.Names = (IDictionary<string, string>)data.Data);

        }


        private bool CanEditObject()
        {
            return !IsReadOnly;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
