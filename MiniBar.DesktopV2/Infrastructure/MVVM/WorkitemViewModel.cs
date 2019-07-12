using Infrastructure.Interface;
using Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MVVM
{
    public class WorkitemViewModel : BaseViewModel, IWorkitemAware
    {


        private IWorkItem workitem;

        public IWorkItem Workitem
        {
            get { return workitem; }
            private set { SetProperty(ref workitem, value, nameof(Workitem)); }
        }

        public void SetWorkitem(IWorkItem workItem)
        {
            Workitem = workItem;

            AppSecurityContext.AppPrincipalChanged += (o, e) => HandleAutheticationStateChanged();
        }

        private void HandleAutheticationStateChanged()
        {
            RaisePropertyChanged(nameof(IsAuthenticated));
            OnAutheticationStateChanged();
        }

        protected virtual void OnAutheticationStateChanged()
        {

        }

        protected bool IsAuthenticated
        {
            get => AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated;
        }

    }
}
