using DevExpress.Mvvm;
using Infrastructure.Interface;
using Infrastructure.Modularity;
using Infrastructure.Workitems;
using MiniBar.Common.Constants;
using MiniBar.Common.Workitems.LanguageEdit.Views;
using Prism.Ioc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace MiniBar.Common.Workitems.LanguageEdit
{
    public class LanguageEditWorkitem : Workitem, ISupportsInitialization
    {

        LanguageEditViewModel LanguageEditViewModel;

        IDictionary<string, string> Data;

        private AsyncCommand saveDataCommand;
        public AsyncCommand SaveDataCommand =>
            saveDataCommand ?? (saveDataCommand = new AsyncCommand(ExecuteSaveDataCommand));

        async Task ExecuteSaveDataCommand()
        {
            Communication.OnNext(new WorkitemEventArgs(this, LanguageEditViewModel.GetData()));
            await Close();
        }

        public LanguageEditWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Language Edit";


        public void Initialize(object data)
        {
            if (data is IDictionary<string, string>)
                Data = (IDictionary<string, string>)data;
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            LanguageEditView view = container.Register<LanguageEditView>(new LanguageEditView(), ScreenRegion.Content);
            LanguageEditViewModel = view.DataContext as LanguageEditViewModel;
            LanguageEditViewModel.SetData(Data);
        }

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(CommandNames.SaveData, SaveDataCommand);
        }

        public override void Configure()
        {
            base.Configure();
            Configuration.Configure(new ModalOptions(new Size(300, 165), ResizeMode.CanResizeWithGrip, WindowStartupLocation.CenterOwner, false, true));
        }

    }
}
