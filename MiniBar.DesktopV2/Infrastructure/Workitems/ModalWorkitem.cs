using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure.Interface;
using Prism.Ioc;

namespace Infrastructure.Workitems
{
    public abstract class ModalWorkitem : WorkitemBase, IModalWorkitem
    {
        public ModalWorkitem(IContainerExtension container) : base(container)
        {
        }

        public IPopup Popup
        {
            get
            {
                return Window as IPopup;
            }
            set
            {
                Window = value as Window;
            }
        }

        public virtual WindowStartupLocation WindowStartupLocation { get; } = WindowStartupLocation.CenterOwner;

        public virtual Size Size
        {
            get
            {
                return new Size(800, 500);
            }
        }

        public virtual bool ShowIcon => false;

        public virtual ResizeMode ResizeMode => ResizeMode.CanResize;
    }
}
