using DevExpress.Xpf.Ribbon;
using Infrastructure.Interface;
using Prism;
using System;
using System.Diagnostics;

namespace Infrastructure.Resources
{
    public class WorkitemRibbonPageCategory : RibbonPageCategory, IActiveAware
    {

        public WorkitemRibbonPageCategory()
        {
        }
        
        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set {
                if (isActive != value)
                {
                    isActive = value;
                    HandleActiveChanged();
                }
            }
        }

        public event EventHandler IsActiveChanged;
        
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            
            HandleActiveChanged();
        }

        private void HandleActiveChanged()
        {
            if (IsActive)
                this.IsVisible = true;
            else
                this.IsVisible = false;
            IsActiveChanged?.Invoke(this, new EventArgs());
        }
    }
}
