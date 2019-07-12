using Infrastructure.Attributes;
using Infrastructure.DX.Attributes;
using Infrastructure.MVVM;
using MiniBar.EntityViewModels.Base;
using MiniBar.EntityViewModels.Global;
using SharedEntities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MiniBar.EntityViewModels.Products
{
    public class ProductUploadViewModel : EntityViewModelBase<ProductUploadViewModel>, IIdEntityViewModel
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

        private IDictionary<string, string> _Names;
        [LanguageDictionary]
        public IDictionary<string, string> Names
        {
            get { return _Names; }
            set
            {
                SetProperty(ref _Names, value, nameof(Names));

                RaisePropertiesChanged(nameof(MainName));
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


        private CategoryViewModel _Category;
        public CategoryViewModel Category
        {
            get { return _Category; }
            set
            {
                SetProperty(ref _Category, value, nameof(Category), () => CategoryID = value?.ID ?? 0);
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

        private BrandViewModel brand;
        public BrandViewModel Brand
        {
            get { return brand; }
            set
            {
                SetProperty(ref brand, value, nameof(Brand), () => BrandID = value?.ID ?? 0);
            }
        }

        private ProductSize size;
        [Range(1, int.MaxValue, ErrorMessage = "Size cannot be unkown")]
        public ProductSize Size
        {
            get { return size; }
            set
            {
                SetProperty(ref size, value, nameof(Size));
            }
        }

        private float _Price;
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public float Price
        {
            get { return _Price; }
            set
            {
                SetProperty(ref _Price, value, nameof(Price));
            }
        }

        private IDictionary<string, string> _Description;
        [LanguageDictionary]
        public IDictionary<string, string> Description
        {
            get { return _Description; }
            set
            {
                SetProperty(ref _Description, value, nameof(Description));
                RaisePropertiesChanged(nameof(MainDescription));
            }
        }


        private BindableDictionary<BindableTuple<string, string>> _LanguageData;
        public BindableDictionary<BindableTuple<string, string>> LanguageData
        {
            get { return _LanguageData; }
            set
            {
                SetProperty(ref _LanguageData, value, nameof(LanguageData));
            }
        }

        [Required]
        public string MainDescription
        {
            get { return (Description != null && Description.ContainsKey("en")) ? Description["en"] : ""; }
        }


        private ImageViewModel image = new ImageViewModel();
        public ImageViewModel Image
        {
            get { return image; }
            set
            {
                SetProperty(ref image, value, nameof(Image));
            }
        }
    }
}
