﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Infrastructure.Framework;
using Infrastructure.Interface;
using MiniBar.Common.Workitems.LanguageEdit;

namespace MiniBar.ProductsModule.Workitems.CategoryManager.Views
{
    partial class CategoryManagerViewModel
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
            channel.Subscribe(data => CurrentItemDetails.Names = (IDictionary<string, string>)data.Data);

        }

        private bool CanEditObject()
        {
            return !IsReadOnly;
        }

    }
}