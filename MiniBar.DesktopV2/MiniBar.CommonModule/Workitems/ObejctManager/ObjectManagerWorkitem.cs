using DevExpress.Spreadsheet;
using DevExpress.Xpf.Ribbon;
using Documents.Exceptions;
using Infrastructure.ChangeTracking;
using Infrastructure.Framework;
using Infrastructure.Interface;
using Infrastructure.Logging;
using Infrastructure.Modularity;
using Infrastructure.Office;
using Infrastructure.Utility;
using Infrastructure.Workitems;
using MiniBar.Common.Resources;
using MiniBar.Common.Workitems.ImportExcel;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MiniBar.Common.Workitems.ObjectManager
{
    public abstract class ObjectManagerWorkitem<TView, TList, TDetails> : Workitem
        where TList : IIdEntityViewModel
        where TDetails : class, IEditableObject, IIdEntityViewModel, new()
        where TView : FrameworkElement
    {
        #region Constructor

        public ObjectManagerWorkitem(IContainerExtension container) : base(container)
        {
        }


        #endregion

        ExcelDocument<TDetails> document;

        #region Properties

        public ObjectManagerViewModel<TList, TDetails> ObjectManagerViewModel { get; private set; }

        public override bool IsDirty => ObjectManagerViewModel?.IsDirty ?? false;

        protected virtual string ImportTemplateName
        {
            get
            {
                return "ImportObjectTemplate";
            }
        }

        #endregion

        #region Public/Protected Methods

        private SecureAsyncCommand importFromExcelCommand;
        public SecureAsyncCommand ImportFromExcelCommand =>
            importFromExcelCommand ?? (importFromExcelCommand = Disposable(new SecureAsyncCommand(ExecuteImportFromExcelCommand, CanExecuteImportFromExcelCommand)));


        async Task ExecuteImportFromExcelCommand()
        {
            var observable = await CurrentContextService.LaunchModalWorkItem<ImportExcelWorkitem>(new ImportExcelOptions(
                (DocumentFormat) =>
                {
                    MemoryStream stream = new MemoryStream();
                    ResolveDocument().GetTemplate().SaveDocument(stream, DocumentFormat);
                    return stream;
                }, ImportTemplateName
                )
            { }
                , this);
            Disposable(observable.Subscribe(HandleImportResult));
        }

        bool CanExecuteImportFromExcelCommand()
        {
            return ObjectManagerViewModel.EditMode == EditMode.Default && ObjectManagerViewModel.IsListEnabled;
        }

        public override void Configure()
        {
            base.Configure();
            Configuration.Configure(new ModalOptions(new Size(800, 600), ResizeMode.CanResize, WindowStartupLocation.CenterOwner, false));
        }

        private async void HandleImportResult(WorkitemEventArgs ev)
        {
            List<TDetails> res = await TaskManager.Run(() =>
            {
                string file = (string)ev.Data;
                Workbook workbook = new Workbook();
                workbook.LoadDocument(file);
                try
                {
                    return ResolveDocument().Parse(workbook);
                }
                catch (ExcelParseException e)
                {
                    Logger.LogErrorSource("Error occured while parsing document", e);
                    UIManager.Error(e.Message);
                }
                catch (Exception e)
                {
                    Logger.LogErrorSource("Unknwon error occured while parsing document", e);
                    UIManager.Error("Unknwon error occured while parsing document");
                }
                return null;
            }).ConfigureAwait(false);
            if (res != null)
                Disposable((await LaunchQCWorkitem(res)).Subscribe(HandleQCResult));
        }

        private void HandleQCResult(WorkitemEventArgs ev)
        {
            if (ev.Data is List<TDetails>)
            {

                List<TDetails> list = ev.Data as List<TDetails>;
                ObjectManagerViewModel.IsListLoading = true;
                AddList(list)
                    .ObserveOn(Application.Current.Dispatcher)
                    .Subscribe(async _ =>
                    {
                        await ObjectManagerViewModel.RefreshItems(ObjectManagerViewModel.CurrentItemDetails?.ID);
                        ObjectManagerViewModel.IsListLoading = false;
                    }, e => ApiHelper.HandleApiException(e, "Failed to add list", () =>
                    {
                        ObjectManagerViewModel.IsListLoading = false;
                    }));
            }
        }


        protected abstract Task<IObservable<WorkitemEventArgs>> LaunchQCWorkitem(List<TDetails> details);

        protected virtual IObservable<System.Reactive.Unit> AddList(List<TDetails> list)
        {
            return Observable.Empty<System.Reactive.Unit>();
        }

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);

            container.Register(Constants.CommandNames.AddObject, ObjectManagerViewModel.AddCommand);
            container.Register(Constants.CommandNames.AddObjectCopy, ObjectManagerViewModel.AddCopyCommand);
            container.Register(Constants.CommandNames.EditObject, ObjectManagerViewModel.EditCommand);
            container.Register(Constants.CommandNames.SaveObject, ObjectManagerViewModel.SaveCommand);
            container.Register(Constants.CommandNames.RemoveObject, ObjectManagerViewModel.DeleteCommand);
            container.Register(Constants.CommandNames.CancelEditingObject, ObjectManagerViewModel.CancelCommand);
            container.Register(Constants.CommandNames.RefreshList, ObjectManagerViewModel.RefreshListCommand);
            container.Register(Constants.CommandNames.Search, ObjectManagerViewModel.SearchCommand);
            container.Register(Constants.CommandNames.ImportFromExcel, ImportFromExcelCommand);
            container.Register(Constants.CommandNames.ExpandAll, ObjectManagerViewModel.ExpandAllCommand);
            container.Register(Constants.CommandNames.CollapseAll, ObjectManagerViewModel.CollapseAllCommand);
        }

        protected virtual ExcelDocument<TDetails> GetDocument()
        {
            return null;
        }


        private ExcelDocument<TDetails> ResolveDocument()
        {
            if (document == null)
            {
                document = GetDocument();
            }
            return document;
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);


            RibbonPageCategory pageCategory = GetRibbonCategory();
            if (pageCategory != null)
            {
                pageCategory.Caption = WorkItemName;
            }

            TView view = Container.Resolve<TView>();
            ObjectManagerViewModel = (ObjectManagerViewModel<TList, TDetails>)view.DataContext;

            Disposable(ObjectManagerViewModel.WhenAnyPropertyChanges(o => o.IsDirty).Subscribe(_ => OnIsDirtyChanged()));
            Disposable(ObjectManagerViewModel.WhenAnyPropertyChanges(o => o.EditMode).Subscribe(_ => ImportFromExcelCommand.RaiseCanExecuteChanged()));
            Disposable(ObjectManagerViewModel.WhenAnyPropertyChanges(o => o.IsListEnabled).Subscribe(_ => ImportFromExcelCommand.RaiseCanExecuteChanged()));
            ObjectManagerActionsToolbarLinks links = new ObjectManagerActionsToolbarLinks();
            container.Register(pageCategory, ScreenRegion.Ribbon);

            container.Register<TView>(view, ScreenRegion.Content);

            container.Register(links, ScreenRegion.Ribbon);

        }

        protected virtual RibbonPageCategory GetRibbonCategory()
        {
            return new ObjectManagerMainPageGroup();
        }

        #endregion
    }
}
