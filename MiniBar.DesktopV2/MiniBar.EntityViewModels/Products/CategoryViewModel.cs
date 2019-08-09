using Infrastructure.Framework;
using Infrastructure.Interface;
using MiniBar.EntityViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace MiniBar.EntityViewModels.Products
{
    public class CategoryViewModel : EntityViewModelBase<CategoryViewModel>, IIdEntityViewModel
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
        [GridColumn]
        [Required]
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value, nameof(Name));
            }
        }

        private int? parentID;
        public int? ParentID
        {
            get { return parentID; }
            set
            {
                SetProperty(ref parentID, value, nameof(ParentID));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
