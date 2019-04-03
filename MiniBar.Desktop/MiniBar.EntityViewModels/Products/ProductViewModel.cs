using DXInfrastructure.Attributes;
using MiniBar.EntityViewModels.Base;
using MiniBar.EntityViewModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MiniBar.EntityViewModels.Products
{
    public class ProductViewModel : EntityViewModelBase<ProductViewModel>, IIdEntityViewModel
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
        [Required]
        [MinLength(3)]
        [GridColumn]
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value, nameof(Name));
            }
        }

        private int _CategoryID;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The Category field is required.")]
        public int CategoryID
        {
            get { return _CategoryID; }
            set
            {
                SetProperty(ref _CategoryID, value, nameof(CategoryID));
            }
        }

        private string _Category;
        [GridColumn]
        public string Category
        {
            get { return _Category; }
            set
            {
                SetProperty(ref _Category, value, nameof(Category));
            }
        }

        private int _BrandID;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The Brand field is required.")]
        public int BrandID
        {
            get { return _BrandID; }
            set
            {
                SetProperty(ref _BrandID, value, nameof(BrandID));
            }
        }

        private string _Brand;
        [GridColumn]
        public string Brand
        {
            get { return _Brand; }
            set
            {
                SetProperty(ref _Brand, value, nameof(Brand));
            }
        }

        private float _Price;
        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public float Price
        {
            get { return _Price; }
            set
            {
                SetProperty(ref _Price, value, nameof(Price));
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                SetProperty(ref _Description, value, nameof(Description));
            }
        }
    }
}
