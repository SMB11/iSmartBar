using DevExpress.Xpf.Ribbon;
using System;

namespace Infrastructure.Resources
{
    public class WorkitemRibbonPageCategory : RibbonPageCategory
    {

        protected WorkitemClosePageGroup ClosePageGroup { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (ClosePageGroup == null && this.Pages.Count > 0 && this.Pages[0].Groups.Count > 0)
            {
                ClosePageGroup = new WorkitemClosePageGroup();
                this.Pages[0].Groups.Add(ClosePageGroup);
            }
        }

    }
}
