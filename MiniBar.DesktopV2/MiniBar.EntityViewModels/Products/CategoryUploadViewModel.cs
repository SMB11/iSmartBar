using Infrastructure.Attributes;
using Infrastructure.DX.Attributes;
using Infrastructure.MVVM;
using MiniBar.EntityViewModels.Base;
using MiniBar.EntityViewModels.Global;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MiniBar.EntityViewModels.Products
{
    public class CategoryUploadViewModel : EntityViewModelBase<CategoryUploadViewModel>, IIdEntityViewModel
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


        [Required]
        public string MainName
        {
            get { return (Names != null && Names.ContainsKey(CultureInfo.CurrentCulture.Name)) ? Names[CultureInfo.CurrentCulture.Name] : ""; }
        }

        private BindableDictionary<string> _Names;
        [LanguageDictionary]
        public BindableDictionary<string> Names
        {
            get { return _Names; }
            set
            {
                SetProperty(ref _Names, value, nameof(Names));

                RaisePropertiesChanged(nameof(MainName));
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

        private CategoryViewModel _Category;
        public CategoryViewModel ParentCategory
        {
            get { return _Category; }
            set
            {
                SetProperty(ref _Category, value, nameof(ParentCategory), () => ParentID = ParentCategory?.ID);
            }
        }
    }
}
