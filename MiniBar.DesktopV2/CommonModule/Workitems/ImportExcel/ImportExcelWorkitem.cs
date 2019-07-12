using DevExpress.Mvvm;
using Infrastructure;
using Infrastructure.Util;
using Infrastructure.Interface;
using Infrastructure.Workitems;
using MiniBar.Common.Workitems.ImportExcel.Views;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Infrastructure.Extensions;

namespace MiniBar.Common.Workitems.ImportExcel
{
    public class ImportExcelWorkitem : ModalWorkitem, ISupportsInitialization
    {
        private ImportExcelOptions ImportExcelOptions;

        public ImportExcelWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Import From Excel";

        public override Size Size => new Size(500,300);

        public ImportViewModel ImportViewModel { get; private set; }

        public void Initialize(object data)
        {
            ImportExcelOptions = (ImportExcelOptions)data;

        }

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(Constants.CommandNames.FinishImport, ImportFromFileCommand);
        }

        private AsyncCommand importFromFileCommand;
        public AsyncCommand ImportFromFileCommand =>
            importFromFileCommand ?? (importFromFileCommand = new AsyncCommand(ExecuteImportFromFileCommand, CanExecuteImportFromFileCommand));

        private bool CanExecuteImportFromFileCommand()
        {
            return !ImportViewModel.IsTemplateLoading;
        }

        async Task ExecuteImportFromFileCommand()
        {
            if (!File.Exists(ImportViewModel.FilePath))
            {
                UIHelper.Error("The specified file doesn't exist");
                return;
            }
            await Parent.OnResultRecieved(this, ImportViewModel.FilePath);

            Close();
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            var view = new ImportView(this);
            ImportViewModel = (ImportViewModel)view.DataContext;
            ImportViewModel.ImportExcelOptions = ImportExcelOptions;
            ImportViewModel.WhenAnyPropertyChanges(v => v.IsTemplateLoading).Subscribe(_ => ImportFromFileCommand.RaiseCanExecuteChanged());
            container.Register(view);
            Popup.SetContent(view);
        }
    }
}
