using MiniBar.EntityViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBar.EntityViewModels.Global
{
    public class ImageViewModel : EntityViewModelBase<ImageViewModel>
    {
        private byte[] bytes;
        public byte[] Bytes
        {
            get
            {
                return bytes;
            }
            set
            {
                SetProperty(ref bytes, value, nameof(Bytes));
            }
        }
    }
}
