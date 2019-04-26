using DXInfrastructure.Attributes;
using MiniBar.EntityViewModels.Base;
using MiniBar.EntityViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBar.EntityViewModels.Products
{
    public class BrandUplaodViewModel : EntityViewModelBase<BrandViewModel>, IIdEntityViewModel
    {

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                SetProperty(ref _ID, value, nameof(ID));
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value, nameof(Name));
            }
        }

        private byte[] _image;
        public byte[] Image
        {
            get { return _image; }
            set
            {
                SetProperty(ref _image, value, nameof(Image));
            }
        }
    }
}
