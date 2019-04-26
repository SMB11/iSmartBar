using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows;
using AutoMapper;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using Infrastructure.Api;
using Infrastructure.Helpers;
using Infrastructure.Interface;
using Infrastructure.MVVM;
using Infrastructure.MVVM.Commands;
using MiniBar.Common.Resources;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using Security.Api;
using SharedEntities.DTO.Product;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Views
{
    class BrandDetailsViewModel : BaseViewModel, IObjectDetailsManager
    {
        
        BrandService BrandService { get; set; }
        public BrandDetailsViewModel(BrandService brandService) : base()
        {
            BrandService = brandService;
        }

        private byte[] oldImage;
        private bool hasObjectImageChanged;
        private byte[] _ObjectImage;
        public byte[] ObjectImage
        {
            get { return _ObjectImage; }
            set
            {
                if (value != _ObjectImage)
                    hasObjectImageChanged = true;
                SetProperty(ref _ObjectImage, value, nameof(ObjectImage));
            }
        }

        private BrandUplaodViewModel oldItem;
        private BrandUplaodViewModel currentItem;
        public BrandUplaodViewModel CurrentItem
        {
            get { return currentItem; }
            set {
                SetProperty(ref currentItem, value, nameof(CurrentItem));

            }
        }

        private bool isReadOnly = true;

        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                SetProperty(ref isReadOnly, value, nameof(IsReadOnly));
            }
        }

        private bool isobjectLoading;

        public bool IsObjectLoading
        {
            get { return isobjectLoading; }
            set
            {
                SetProperty(ref isobjectLoading, value, nameof(IsObjectLoading));
            }
        }

        private bool CanEdit()
        {
            return !IsReadOnly;
        }


        #region IObjectDetailsManager
        object IObjectDetailsManager.EditingItem {
            get {
                if (!this.IsReadOnly) {
                    if (hasObjectImageChanged)
                    {
                        CurrentItem.Image = ObjectImage;
                    }
                    return CurrentItem;
                }
                return null;
            }
        }
        

        void IObjectDetailsManager.BeginAdd()
        {
            oldItem = CurrentItem;
            oldImage = ObjectImage;
            CurrentItem = new BrandUplaodViewModel();
            IsReadOnly = false;
        }

        void IObjectDetailsManager.BeginEdit()
        {
            oldImage = ObjectImage;
            IsReadOnly = false;
        }

        void IObjectDetailsManager.ChangeCurrentItem(object currentItem)
        {

            ObjectImage = null;
            hasObjectImageChanged = false;
            if (currentItem == null)
            {
                CurrentItem = null;
                
            }
            else
            {
                IObservable<BrandUploadDTO> obs = Observable.FromAsync(() => BrandService.GetForUploadByID((currentItem as BrandViewModel).ID));
                obs.Subscribe(dto => {
                    CurrentItem = new BrandUplaodViewModel()
                    {
                        ID = dto.ID,
                        Name = dto.Name
                    };

                    if (dto.ImagePath != null)
                    {
                        IObservable<byte[]> imageObs = Observable.FromAsync(() => ApiImageHelper.GetImageBytesAsync(dto.ImagePath));
                        imageObs.Subscribe((img) => {
                            ObjectImage = img;
                            hasObjectImageChanged = false;
                        }, (e) => {
                            ApiHelper.HandleApiException(e);
                        });
                    }
                }, ex => {

                    ApiHelper.HandleApiException(ex);
                });
                
            }
            
        }
        

        void IObjectDetailsManager.Cancel()
        {
            if (oldItem != null)
            {
                CurrentItem = oldItem;
                oldItem = null;
            }

            ObjectImage = oldImage;
            IsReadOnly = true;
        }
        #endregion
    }
}
