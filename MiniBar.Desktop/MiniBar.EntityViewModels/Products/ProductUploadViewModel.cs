﻿using DXInfrastructure.Attributes;
using MiniBar.EntityViewModels.Base;
using MiniBar.EntityViewModels.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
            get { return (Names != null && Names.ContainsKey("en"))? Names["en"]: ""; }
        }

        private Dictionary<string, string> _Names;
        public Dictionary<string, string> Names
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

        private Dictionary<string, string> _Description;
        public Dictionary<string, string> Description
        {
            get { return _Description; }
            set
            {
                SetProperty(ref _Description, value, nameof(Description));
                RaisePropertiesChanged(nameof(MainDescription));
            }
        }

        [Required]
        public string MainDescription
        {
            get { return (Description != null && Description.ContainsKey("en")) ? Description["en"] : ""; }
        }
    }
}