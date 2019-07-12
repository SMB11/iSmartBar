using Infrastructure;
using Infrastructure.Interface;
using Infrastructure.MVVM;
using Infrastructure.Util;
using Infrastructure.Workitems;
using MiniBar.Common.Workitems.EntityQC.Views;
using MiniBar.EntityViewModels.Base;
using Prism.Ioc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiniBar.Common.Workitems.EntityQC
{
    public abstract class EntityQCWorkitem<T> : ModalWorkitem, ISupportsInitialization
    {
        protected QCViewModel QCViewModel;
        protected IList List;

        public EntityQCWorkitem(IContainerExtension container) : base(container)
        {
        }


        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            var view = CreateView();
            QCViewModel = (QCViewModel)view.DataContext;
            QCViewModel.List = new System.Collections.ObjectModel.ObservableCollection<object>(List.OfType<object>().ToList());
            container.Register(view);
            Popup.SetContent(view);
        }

        protected abstract FrameworkElement CreateView();

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(Infrastructure.Constants.CommandNames.Search, QCViewModel.SearchCommand);
            container.Register(Infrastructure.Constants.CommandNames.ExpandAll, QCViewModel.ExpandAllCommand);
            container.Register(Infrastructure.Constants.CommandNames.CollapseAll, QCViewModel.CollapseAllCommand);
            container.Register(Infrastructure.Constants.CommandNames.RemoveObject, QCViewModel.RemoveCommand);
            container.Register(Infrastructure.Constants.CommandNames.Accept, AcceptCommand);
        }



        private SecureCommand acceptCommand;
        public SecureCommand AcceptCommand =>
            acceptCommand ?? (acceptCommand = new SecureCommand(Accept));

        private void Accept()
        {
            if (!Validate())
            {
                UIHelper.Error("Fix errors before saving");
                return;
            }
            Parent.OnResultRecieved(this, new List<T>(QCViewModel.List.OfType<T>()));

            Close();
        }


        protected bool Validate()
        {
            foreach (var item in List.OfType<IDataErrorInfoExtended>())
                if (item.HasErrors)
                    return false;
            return true;
        }

        public override void Run()
        {
            base.Run();
        }

        public virtual void Initialize(object data)
        {
            if(data is IList)
            {
                List = data as IList;
            }
        }
    }
}
