using Core.Documents.Excel;
using Core.Documents.Exceptions;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Ribbon;
using Infrastructure;
using Infrastructure.Interface;
using Infrastructure.MVVM;
using Infrastructure.Prism;
using Infrastructure.Resources;
using Infrastructure.Workitems;
using MiniBar.Common.Workitems.EntityQC;
using MiniBar.Common.Workitems.ImportExcel;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Infrastructure.Extensions;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure.Util;
using System.Reactive.Linq;
using Infrastructure.Connection;
using MiniBar.Common.MVVM;

namespace MiniBar.Common.Workitems
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

        public override bool IsDirty => ObjectManagerViewModel.IsDirty;

        protected virtual string ImportTemplateName
        {
            get
            {
                return "ImportObjectTemplate";
            }
        }

        #endregion

        #region Public/Protected Methods

        private SecureCommand importFromExcelCommand;
        public SecureCommand ImportFromExcelCommand =>
            importFromExcelCommand ?? (importFromExcelCommand = new SecureCommand(ExecuteImportFromExcelCommand, CanExecuteImportFromExcelCommand));

        void ExecuteImportFromExcelCommand()
        {
            CurrentContextService.LaunchWorkItem<ImportExcelWorkitem>(new ImportExcelOptions(
                (DocumentFormat) =>
                {
                    MemoryStream stream = new MemoryStream();
                    ResolveDocument().GetTemplate().SaveDocument(stream, DocumentFormat);
                    return stream;
                }, ImportTemplateName
                )
            { }
                , this);
        }

        bool CanExecuteImportFromExcelCommand()
        {
            return ObjectManagerViewModel.EditMode == Infrastructure.Enums.EditMode.Default && ObjectManagerViewModel.IsListEnabled;
        }


        public async override Task OnResultRecieved(IWorkItem child, object result)
        {
            await base.OnResultRecieved(child, result);
            if (child is ImportExcelWorkitem)
            {
                string file = (string)result;
                Workbook workbook = new Workbook();
                workbook.LoadDocument(file);
                List<TDetails> res = null;
                try
                {
                    res = ResolveDocument().Parse(workbook);
                }
                catch (ExcelParseException e)
                {
                    UIHelper.Error(e.Message);
                    return;
                }
                catch(Exception e)
                {
                    UIHelper.Error("Error occured while parsing document.");
                    return;
                }
                LaunchQCWorkitem(res);

            }
            else if (child is EntityQCWorkitem<TDetails>)
            {
                if (result is List<TDetails>) { 

                    List<TDetails> list = result as List<TDetails>;
                    ObjectManagerViewModel.IsListLoading = true;
                    AddList(list)
                        .Subscribe(async _ => {
                            await ObjectManagerViewModel.RefreshItems(ObjectManagerViewModel.CurrentItemDetails?.ID);
                            ObjectManagerViewModel.IsListLoading = false;
                        }, e => ApiHelper.HandleApiException(e, "Failed to add list", () => {
                            ObjectManagerViewModel.IsListLoading = false;
                        }));
                }
            }
        }

        protected abstract void LaunchQCWorkitem(List<TDetails> details);

        protected virtual IObservable<System.Reactive.Unit> AddList(List<TDetails> list)
        {
            return Observable.Empty<System.Reactive.Unit>();
        }

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(Infrastructure.Constants.CommandNames.AddObject, ObjectManagerViewModel.AddCommand);
            container.Register(Infrastructure.Constants.CommandNames.AddObjectCopy, ObjectManagerViewModel.AddCopyCommand);
            container.Register(Infrastructure.Constants.CommandNames.EditObject, ObjectManagerViewModel.EditCommand);
            container.Register(Infrastructure.Constants.CommandNames.SaveObject, ObjectManagerViewModel.SaveCommand);
            container.Register(Infrastructure.Constants.CommandNames.RemoveObject, ObjectManagerViewModel.DeleteCommand);
            container.Register(Infrastructure.Constants.CommandNames.CancelEditingObject, ObjectManagerViewModel.CancelCommand);
            container.Register(Infrastructure.Constants.CommandNames.RefreshList, ObjectManagerViewModel.RefreshListCommand);
            container.Register(Infrastructure.Constants.CommandNames.Search, ObjectManagerViewModel.SearchCommand);
            container.Register(Infrastructure.Constants.CommandNames.ImportFromExcel, ImportFromExcelCommand);
            container.Register(Infrastructure.Constants.CommandNames.ExpandAll, ObjectManagerViewModel.ExpandAllCommand);
            container.Register(Infrastructure.Constants.CommandNames.CollapseAll, ObjectManagerViewModel.CollapseAllCommand);
        }

        protected virtual ExcelDocument<TDetails> GetDocument()
        {
            return null;
        }


        private ExcelDocument<TDetails> ResolveDocument()
        {
            if(document == null)
            {
                document = GetDocument();
            }
            return document;
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            TView view = container.Register<TView>(Container.Resolve<TView>());
            ObjectManagerViewModel = (ObjectManagerViewModel<TList, TDetails>)view.DataContext;
            RegionManager.AddTab(view, WorkItemName, this);

            Disposable(ObjectManagerViewModel.WhenAnyPropertyChanges(o => o.IsDirty).Subscribe(_ => OnIsDirtyChanged()));
            Disposable(ObjectManagerViewModel.WhenAnyPropertyChanges(o => o.EditMode).Subscribe(_ => ImportFromExcelCommand.RaiseCanExecuteChanged()));
            Disposable(ObjectManagerViewModel.WhenAnyPropertyChanges(o => o.IsListEnabled).Subscribe(_ => ImportFromExcelCommand.RaiseCanExecuteChanged()));


            var links = new ObjectManagerActionsToolbarLinks();
            container.Register(links);
            RegionManager.AddToolbar(links, this);

        }

        protected override RibbonPageCategory GetRibbonCategory()
        {
            return new ObjectManagerMainPageGroup();
        }

        #endregion
    }
}
