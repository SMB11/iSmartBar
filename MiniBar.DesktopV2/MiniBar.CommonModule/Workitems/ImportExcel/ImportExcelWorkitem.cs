using DevExpress.Mvvm;
using Infrastructure.Interface;
using Infrastructure.Modularity;
using Infrastructure.Utility;
using Infrastructure.Workitems;
using MiniBar.Common.Workitems.ImportExcel.Views;
using Prism.Ioc;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace MiniBar.Common.Workitems.ImportExcel
{
    public class ImportExcelWorkitem : Workitem, ISupportsInitialization
    {
        private ImportExcelOptions ImportExcelOptions;

        public ImportExcelWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Import From Excel";

        public override void Configure()
        {
            base.Configure();
            Configuration.Configure(new ModalOptions(new Size(500, 300), ResizeMode.CanResize, WindowStartupLocation.CenterOwner, false, true));
        }

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
                UIManager.Error("The specified file doesn't exist");
                return;
            }
            Communication.OnNext(new WorkitemEventArgs(this, ImportViewModel.FilePath));

            await Close();
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            var view = container.Register<ImportView>(new ImportView(), ScreenRegion.Content);
            ImportViewModel = (ImportViewModel)view.DataContext;
            ImportViewModel.ImportExcelOptions = ImportExcelOptions;
            ImportViewModel.WhenAnyPropertyChanges(v => v.IsTemplateLoading).Subscribe(_ => ImportFromFileCommand.RaiseCanExecuteChanged());

        }
    }
}
