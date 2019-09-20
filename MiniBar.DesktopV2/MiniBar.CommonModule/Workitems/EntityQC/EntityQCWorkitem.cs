using Infrastructure.Framework;
using Infrastructure.Interface;
using Infrastructure.Modularity;
using Infrastructure.Workitems;
using MiniBar.Common.Resources;
using MiniBar.Common.Workitems.EntityQC.Views;
using Prism.Ioc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MiniBar.Common.Workitems.EntityQC
{
    public abstract class EntityQCWorkitem<T> : Workitem, ISupportsInitialization
    {
        protected QCViewModel QCViewModel;
        protected IList List;

        public EntityQCWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override void Configure()
        {
            base.Configure();
            Configuration.Configure(new ModalOptions(new Size(800, 600), ResizeMode.CanResize, WindowStartupLocation.CenterOwner, false, true));
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            var view = container.Register<FrameworkElement>(CreateView(), ScreenRegion.Content);
            QCViewModel = (QCViewModel)view.DataContext;
            QCViewModel.List = new System.Collections.ObjectModel.ObservableCollection<object>(List.OfType<object>().ToList());

            QARibbonPageCategory category = new QARibbonPageCategory();
            category.Caption = WorkItemName;
            container.Register(category, region: ScreenRegion.Ribbon);

        }

        protected abstract FrameworkElement CreateView();

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(Constants.CommandNames.Search, QCViewModel.SearchCommand);
            container.Register(Constants.CommandNames.ExpandAll, QCViewModel.ExpandAllCommand);
            container.Register(Constants.CommandNames.CollapseAll, QCViewModel.CollapseAllCommand);
            container.Register(Constants.CommandNames.RemoveObject, QCViewModel.RemoveCommand);
            container.Register(Constants.CommandNames.Accept, AcceptCommand);
        }



        private SecureCommand acceptCommand;
        public SecureCommand AcceptCommand =>
            acceptCommand ?? (acceptCommand = Disposable(new SecureCommand(Accept)));

        private void Accept()
        {
            if (!Validate())
            {
                UIManager.Error("Fix errors before saving");
                return;
            }
            Communication.OnNext(new WorkitemEventArgs(this, new List<T>(QCViewModel.List.OfType<T>())));

            Close();
        }


        protected bool Validate()
        {
            foreach (var item in List.OfType<IDataErrorInfoExtended>())
                if (item.HasErrors)
                    return false;
            return true;
        }

        public virtual void Initialize(object data)
        {
            if (data is IList)
            {
                List = data as IList;
            }
        }
    }
}
