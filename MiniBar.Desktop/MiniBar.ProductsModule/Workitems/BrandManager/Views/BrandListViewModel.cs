using AutoMapper;
using Infrastructure.Helpers;
using Infrastructure.Interface;
using Infrastructure.MVVM;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using Prism.Events;
using Security.Api;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Views
{
    class BrandListViewModel : BaseViewModel, IObjectListManager
    {
        BrandService BrandService { get; set; }

        public BrandListViewModel(BrandService brandService): base()
        {
            BrandService = brandService;
        }


        private ObservableCollection<BrandViewModel> brands;

        public ObservableCollection<BrandViewModel> Brands
        {
            get { return brands; }
            set { SetProperty(ref brands, value, nameof(Brands)); }
        }

        private bool isEnabled = true;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value, nameof(IsEnabled)); }
        }



        private bool isListLoading;

        public bool IsListLoading
        {
            get { return isListLoading; }
            set { SetProperty(ref isListLoading, value, nameof(IsListLoading)); }
        }


        private BrandViewModel currentItem;
        public BrandViewModel CurrentItem
        {
            get { return currentItem; }
            set {
                currentItem = value;
                SetProperty(ref currentItem, value, nameof(CurrentItem));
                whenCurrentItemChanges.OnNext(value);
                Debug.WriteLine(value == null? "null": ""+value.ID);
            }
        }

        #region IObjectListManager

        object IObjectListManager.CurrentItem
        {
            get
            {
                return CurrentItem;
            }
        }

        void IObjectListManager.RemoveByID(int id)
        {
            if (Brands != null)
            {
                BrandViewModel brand = Brands.Where(p => p.ID == id).FirstOrDefault();
                if (brand != null)
                {
                    int index = Brands.IndexOf(brand);
                    UIHelper.ExecuteUIThread(() => {
                        Brands.RemoveAt(index);
                    });

                    if (Brands.Count > 0)
                    {
                        CurrentItem = Brands[Math.Max(0, index - 1)];
                    }
                }
            }
        }

        private Subject<object> whenCurrentItemChanges = new Subject<object>();
        IObservable<object> IObjectListManager.WhenCurrentItemChanges
        {
            get
            {
                return whenCurrentItemChanges.Throttle(TimeSpan.FromSeconds(0.2)).AsObservable();
            }
        }

        void IObjectListManager.Disable()
        {
            IsEnabled = false;
        }

        void IObjectListManager.Enable()
        {
            IsEnabled = true;
        }


        void IObjectListManager.RefreshItems(int? selectID = null)
        {
            IsListLoading = true;
            IObservable<List<BrandDTO>> obs = Observable.FromAsync(() => BrandService.GetAll());
            obs.Subscribe(list => {
                Brands = Mapper.Map<ObservableCollection<BrandViewModel>>(list);

                BrandViewModel selection = Brands.FirstOrDefault(p => p.ID == selectID);
                if (selection != null)
                    CurrentItem = selection;
                else
                    CurrentItem = Brands.FirstOrDefault();
                IsListLoading = false;
            }, ex =>
            {
                ApiHelper.HandleApiException(ex);
                IsListLoading = false;
            });
        }
        #endregion
    }
}
